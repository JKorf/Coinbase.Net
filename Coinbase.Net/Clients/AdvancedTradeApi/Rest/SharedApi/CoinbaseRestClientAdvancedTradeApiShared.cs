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
    internal partial class CoinbaseRestClientAdvancedTradeSharedApi : 
        SharedApiBase,
        ICoinbaseRestClientAdvancedTradeApiShared,
        ICoinbaseRestClientAdvancedTradeSharedApi
    {
        private readonly CoinbaseRestClientAdvancedTradeApi _api;

        private const string _topicSpotId = "CoinbaseSpot";
        private const string _topicFuturesId = "CoinbaseFutures";
        private const string _exchangeName = "Coinbase";
        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(CoinbaseExchange.Metadata, this);

        private static readonly HashSet<string> _exchangeSupportedFiat = ["USD", "EUR", "GBP", "INR", "AUD", "CAD", "SGD"];

        public CoinbaseRestClientAdvancedTradeSharedApi(CoinbaseRestClientAdvancedTradeApi api)
           : base(
                 SharedTransport.Rest,
                 api.Exchange,
                 [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                 () => api.Authenticated,
                 api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetAssetOptions,
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetDepositAddressesOptions,
                GetDepositHistoryOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetTradeHistoryOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetSpotSymbolsOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetFuturesSymbolsOptions,
                GetOpenInterestOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetKlinesOptions,
                GetFeeOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions
                );
        }
    }
}
