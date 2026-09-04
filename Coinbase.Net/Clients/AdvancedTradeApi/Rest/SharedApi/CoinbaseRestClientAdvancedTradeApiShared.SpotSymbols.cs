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
        #region Get Spot Symbols

        async Task<ICallResult<SharedSpotSymbol[]>> IGetSpotSymbols.GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
            => await GetSpotSymbolsAsync(request, ct).ConfigureAwait(false);

        public SharedSymbolCatalog? SpotSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicSpotId, _api.EnvironmentName, null);
        public GetSpotSymbolsOptions GetSpotSymbolsOptions { get; } = new GetSpotSymbolsOptions(_exchangeName, false);

        public async Task<HttpResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetSpotSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedSpotSymbol[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetSymbolsAsync(Enums.SymbolType.Spot, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedSpotSymbol[]>(result);

            // Coinbase return duplicate spot symbols for some pairs
            // For example both BTC-USD and BTC-USDC is returned, referring to the same symbol
            // Also, when for example subscribing to BTC-USDC in update the name is BTC-USD instead
            // The library uses the BTC-USDC notation
            var data = result.Data
                .Select(x => ParseSpotSymbol(x))
                .ToArray();

            var resultData = data.Where(x => x.QuoteAsset != "USD").ToArray();
            foreach (var item in data.Where(x => x.QuoteAsset == "USD"))
                item.QuoteAsset = "USDC";

            ExchangeSymbolCache.UpdateSymbolInfo(_topicSpotId, _api.EnvironmentName, null, data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(resultData, request));
        }

        #endregion

        private SharedSpotSymbol ParseSpotSymbol(CoinbaseSymbol s)
        {
            var result = new SharedSpotSymbol(s.BaseAsset, s.QuoteAsset, s.Symbol, s.SymbolStatus == SymbolStatus.Online && !s.IsDisabled && !s.TradingDisabled)
            {
                MinTradeQuantity = s.MinOrderQuantity,
                MaxTradeQuantity = s.MaxOrderQuantity,
                QuantityStep = s.QuantityStep,
                PriceStep = s.PriceStep,
                DisplayName = s.DisplayName,
            };

            if (_exchangeSupportedFiat.Contains(s.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(s.QuoteAsset))
                    result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            if (_exchangeSupportedFiat.Contains(s.BaseAsset))
            {
                result.BaseAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.BaseAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(s.BaseAsset))
                    result.BaseAssetSubType = SharedAssetSubType.StableCoin;
            }

            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetSpotSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicSpotId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode != TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Only Spot symbols allowed");

            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsSpotSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicSpotId, _api.EnvironmentName, null))
            {
                var symbols = await GetSpotSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicSpotId, _api.EnvironmentName, null, symbolName));
        }
    }
}
