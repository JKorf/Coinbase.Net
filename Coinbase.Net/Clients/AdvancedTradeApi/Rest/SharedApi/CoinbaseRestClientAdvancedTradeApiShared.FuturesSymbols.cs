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

        #region Get Futures Symbols

        async Task<ICallResult<SharedFuturesSymbol[]>> IGetFuturesSymbols.GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
            => await GetFuturesSymbolsAsync(request, ct).ConfigureAwait(false);

        public SharedSymbolCatalog? FuturesSymbolCatalog => ExchangeSymbolCache.GetSymbolCatalog(_exchangeName, _topicFuturesId, _api.EnvironmentName, null);
        public GetFuturesSymbolsOptions GetFuturesSymbolsOptions { get; } = new GetFuturesSymbolsOptions(_exchangeName, false);
        public async Task<HttpResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(GetSymbolsRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesSymbolsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesSymbol[]>(Exchange, validationError);

            var expiringTime = request.TradingMode == null || request.TradingMode == TradingMode.PerpetualLinear ? ContractExpiryType.Perpetual : ContractExpiryType.Expiring;
            var result = await _api.ExchangeData.GetSymbolsAsync(SymbolType.Futures, expiryType: expiringTime, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFuturesSymbol[]>(result);

            var data = result.Data
                .Select(x => ParseFuturesSymbol(x))
                .ToArray();

            ExchangeSymbolCache.UpdateSymbolInfo(_topicFuturesId, _api.EnvironmentName, expiringTime.ToString(), data);
            return HttpResult.Ok(result, SharedUtils.ApplySymbolFilter(data, request));
        }

        #endregion

        private SharedFuturesSymbol ParseFuturesSymbol(CoinbaseSymbol x)
        {
            var result = new SharedFuturesSymbol(
                        x.FutureProductDetails!.ContractExpiry == null ? TradingMode.PerpetualLinear : TradingMode.DeliveryLinear,
                        x.FutureProductDetails.ContractCode,
                        x.QuoteAsset,
                        x.Symbol,
                        x.SymbolStatus == SymbolStatus.Online && !x.IsDisabled && !x.TradingDisabled)
            {
                MinTradeQuantity = x.MinOrderQuantity,
                MaxTradeQuantity = x.MaxOrderQuantity,
                QuantityStep = x.QuantityStep,
                PriceStep = x.PriceStep,
                ContractSize = x.FutureProductDetails.ContractSize,
                DeliveryTime = x.FutureProductDetails.ContractExpiry,
                MaxLongLeverage = x.FutureProductDetails.PerpetualDetails?.MaxLeverage,
                MaxShortLeverage = x.FutureProductDetails.PerpetualDetails?.MaxLeverage,
                DisplayName = x.DisplayName
            };

            if (_exchangeSupportedFiat.Contains(x.QuoteAsset))
            {
                result.QuoteAssetType = SharedAssetType.Fiat;
            }
            else
            {
                result.QuoteAssetType = SharedAssetType.Crypto;
                if (LibraryHelpers.IsStableCoin(x.QuoteAsset))
                    result.QuoteAssetSubType = SharedAssetSubType.StableCoin;
            }

            if (x.FutureProductDetails == null)
            {
                // Shouldn't be null for futures symbols, but just in case
                result.BaseAssetType = SharedAssetType.Unspecified;
            }
            else
            {
                if (result.TradingMode.IsPerpetual())
                {
                    if (x.FutureProductDetails.PerpetualDetails?.UnderlyingType == UnderlyingType.Equity
                        || x.FutureProductDetails.PerpetualDetails?.UnderlyingType == UnderlyingType.EquityEtf
                        || x.FutureProductDetails.PerpetualDetails?.UnderlyingType == UnderlyingType.Index)
                    {
                        result.BaseAssetType = SharedAssetType.TradFi;
                        result.BaseAssetSubType = SharedAssetSubType.Equity;
                    }
                    else if (x.FutureProductDetails.PerpetualDetails?.UnderlyingType == UnderlyingType.Commodity)
                    {
                        result.BaseAssetType = SharedAssetType.TradFi;
                        result.BaseAssetSubType = SharedAssetSubType.Commodity;
                    }
                    else
                    {
                        result.BaseAssetType = SharedAssetType.Crypto;
                        if (LibraryHelpers.IsStableCoin(x.BaseAsset))
                            result.BaseAssetSubType = SharedAssetSubType.StableCoin;
                    }
                }
                else
                {
                    if (x.FutureProductDetails.FuturesAssetType == FuturesAssetType.Stocks
                        || x.FutureProductDetails.FuturesAssetType == FuturesAssetType.Commodities)
                    {
                        result.BaseAssetType = SharedAssetType.TradFi;
                        result.BaseAssetSubType = SharedAssetSubType.Equity;
                    }
                    else if (x.FutureProductDetails.FuturesAssetType == FuturesAssetType.Energy
                        || x.FutureProductDetails.FuturesAssetType == FuturesAssetType.Metals
                        || x.FutureProductDetails.FuturesAssetType == FuturesAssetType.Commodities)
                    {
                        result.BaseAssetType = SharedAssetType.TradFi;
                        result.BaseAssetSubType = SharedAssetSubType.Commodity;
                    }
                    else
                    {
                        result.BaseAssetType = SharedAssetType.Crypto;
                    }
                }
            }


            return result;
        }

        public async Task<ExchangeCallResult<SharedSymbol[]>> GetFuturesSymbolsForBaseAssetAsync(string baseAsset)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<SharedSymbol[]>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<SharedSymbol[]>.Ok(Exchange, ExchangeSymbolCache.GetSymbolsForBaseAsset(_topicFuturesId, _api.EnvironmentName, null, baseAsset));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(SharedSymbol symbol)
        {
            if (symbol.TradingMode == TradingMode.Spot)
                throw new ArgumentException(nameof(symbol), "Spot symbols not allowed");

            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbol));
        }

        public async Task<ExchangeCallResult<bool>> SupportsFuturesSymbolAsync(string symbolName)
        {
            if (!ExchangeSymbolCache.HasCached(_topicFuturesId, _api.EnvironmentName, null))
            {
                var symbols = await GetFuturesSymbolsAsync(new GetSymbolsRequest(), default).ConfigureAwait(false);
                if (!symbols.Success)
                    return ExchangeCallResult<bool>.Fail(Exchange, symbols.Error!);
            }

            return ExchangeCallResult<bool>.Ok(Exchange, ExchangeSymbolCache.SupportsSymbol(_topicFuturesId, _api.EnvironmentName, null, symbolName));
        }

    }
}
