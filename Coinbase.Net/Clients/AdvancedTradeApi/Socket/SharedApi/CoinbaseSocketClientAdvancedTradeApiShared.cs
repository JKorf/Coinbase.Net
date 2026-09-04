using CryptoExchange.Net.SharedApis;
using System;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using System.Linq;
using Coinbase.Net.Enums;
using CryptoExchange.Net;
using Coinbase.Net.Objects.Models;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    internal partial class CoinbaseSocketClientAdvancedTradeSharedApi :
        SharedApiBase,
        ICoinbaseSocketClientAdvancedTradeApiShared,
        ICoinbaseSocketClientAdvancedTradeSharedApi
    {
        private readonly CoinbaseSocketClientAdvancedTradeApi _api;

        private const string _topicSpotId = "CoinbaseSpot";
        private const string _topicFuturesId = "CoinbaseFutures";
        private const string _exchangeName = "Coinbase";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(CoinbaseExchange.Metadata, this);

        public CoinbaseSocketClientAdvancedTradeSharedApi(CoinbaseSocketClientAdvancedTradeApi api)
           : base(
                 SharedTransport.Socket,
                 api.Exchange,
                 [TradingMode.Spot, TradingMode.PerpetualLinear, TradingMode.DeliveryLinear],
                 () => api.Authenticated,
                 api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeTickerOptions,
                SubscribeKlineOptions,
                SubscribeTradeOptions,
                SubscribeSpotOrderOptions,
                SubscribeFuturesOrderOptions,
                SubscribePositionOptions
                );
        }
        
    }
}
