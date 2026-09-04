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
        #region Get Balances

        async Task<ICallResult<SharedBalance[]>> IGetBalances.GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
            => await GetBalancesAsync(request, ct).ConfigureAwait(false);

        public GetBalancesOptions GetBalancesOptions { get; } = new GetBalancesOptions(_exchangeName, AccountTypeFilter.Spot, AccountTypeFilter.Futures);

        public async Task<HttpResult<SharedBalance[]>> GetBalancesAsync(GetBalancesRequest request, CancellationToken ct)
        {
            var validationError = GetBalancesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedBalance[]>(Exchange, validationError);

            if (request.AccountType == SharedAccountType.Spot || request.AccountType == null)
            {
                var result = await _api.Account.GetAccountsAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, result.Data.Accounts.Where(x => x.Type == AccountType.Crypto || x.Type == AccountType.Fiat).Select(x => 
                    new SharedBalance(
                        TradingMode.Spot,
                        x.Asset,
                        x.AvailableBalance.Value,
                        x.AvailableBalance.Value + x.HoldBalance.Value)).ToArray());
            }
            else if (request.AccountType == SharedAccountType.PerpetualLinearFutures || request.AccountType == SharedAccountType.PerpetualInverseFutures)
            {
                var portfolioId = ExchangeParameters.GetValue<string>(request.ExchangeParameters, Exchange, "PortfolioId");
                if (portfolioId == default)
                    return HttpResult.Fail<SharedBalance[]>(Exchange, ArgumentError.Missing("PortfolioId", "PortfolioId is required as Exchange parameter for retrieving Perpetual futures balances"));

                var result = await _api.Account.GetPerpetualBalancesAsync(portfolioId, ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                // The endpoint answers with one entry per portfolio; take the requested one, falling
                // back to the single entry a portfolio-scoped key returns without echoing its uuid.
                var portfolio = result.Data.FirstOrDefault(x => x.PortfolioId == portfolioId)
                    ?? result.Data.FirstOrDefault();
                if (portfolio == null)
                    return HttpResult.Ok(result, Array.Empty<SharedBalance>());

                return HttpResult.Ok(result, portfolio.Balances.Select(x =>
                    new SharedBalance(
                        TradingMode.PerpetualLinear,
                        x.Asset.AssetName,
                        x.MaxWithdrawQuantity,
                        x.Quantity)).ToArray());
            }
            else
            {
                // Delivery futures
                var result = await _api.Account.GetFuturesBalanceSummaryAsync(ct: ct).ConfigureAwait(false);
                if (!result.Success)
                    return HttpResult.Fail<SharedBalance[]>(result);

                return HttpResult.Ok(result, new[] { 
                    new SharedBalance(
                        TradingMode.DeliveryLinear,
                        result.Data.CfmUsdBalance.Asset,
                        result.Data.CfmUsdBalance.Value,
                        result.Data.TotalUsdBalance.Value) });
            }
        }

        #endregion

    }
}
