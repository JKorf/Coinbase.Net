using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Objects.Errors;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseRestClientAdvancedTradeSharedApi
    {
        #region Futures Ticker client

        public GetFuturesTickerOptions GetFuturesTickerOptions { get; } = new GetFuturesTickerOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker>> GetFuturesTickerAsync(GetTickerRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTickerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker>(Exchange, validationError);

            var resultTicker = await _api.ExchangeData.GetSymbolAsync(request.Symbol!.GetSymbol(FormatSymbol), ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedFuturesTicker>(resultTicker);

            return HttpResult.Ok(resultTicker, 
                new SharedFuturesTicker(
                    ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, resultTicker.Data.Symbol),
                    resultTicker.Data.Symbol,
                    resultTicker.Data.LastPrice,
                    resultTicker.Data.HighPrice24h, 
                    resultTicker.Data.LowPrice24h,
                    new SharedOrderQuantity(resultTicker.Data.Volume24h, resultTicker.Data.ApproximateQuote24hVolume),
                    resultTicker.Data.PricePercentageChange24h)
            {
                // Null-conditional, not null-forgiving: both properties are declared nullable, and a
                // DATED CDE contract (XPP-20DEC30-CDE) legitimately carries no PerpetualDetails —
                // funding is a perpetual-swap mechanism, a dated future converges by expiry instead.
                // The `!` pair threw a NullReferenceException out of the shared client for every dated
                // futures symbol (observed 2026-08-04). No funding is the honest answer, not a crash.
                FundingRate = resultTicker.Data.FutureProductDetails?.PerpetualDetails?.FundingRate,
                NextFundingTime = resultTicker.Data.FutureProductDetails?.PerpetualDetails?.FundingTime
            });
        }

        Task<HttpResult<SharedFuturesTicker[]>> IFuturesTickerRestClient.GetFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
            => GetAllFuturesTickersAsync(request, ct);
        GetAllFuturesTickersOptions IFuturesTickerRestClient.GetFuturesTickersOptions => GetAllFuturesTickersOptions;

        public GetAllFuturesTickersOptions GetAllFuturesTickersOptions { get; } = new GetAllFuturesTickersOptions(_exchangeName);
        public async Task<HttpResult<SharedFuturesTicker[]>> GetAllFuturesTickersAsync(GetTickersRequest request, CancellationToken ct)
        {
            var validationError = GetAllFuturesTickersOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTicker[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var resultTicker = await _api.ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!resultTicker.Success)
                return HttpResult.Fail<SharedFuturesTicker[]>(resultTicker);

            var data = resultTicker.Data;
            return HttpResult.Ok(resultTicker, data.Select(x => 
                    new SharedFuturesTicker(
                        ExchangeSymbolCache.ParseSymbol(_topicFuturesId, _api.EnvironmentName, null, x.Symbol),
                        x.Symbol,
                        x.LastPrice,
                        x.HighPrice24h, 
                        x.LowPrice24h, 
                        new SharedOrderQuantity(x.Volume24h, x.ApproximateQuote24hVolume),
                        x.PricePercentageChange24h)
                    {
                        // Same dated-CDE null-safety as GetFuturesTickerAsync above.
                        FundingRate = x.FutureProductDetails?.PerpetualDetails?.FundingRate,
                        NextFundingTime = x.FutureProductDetails?.PerpetualDetails?.FundingTime
                    }).ToArray());
        }

        #endregion
    }
}
