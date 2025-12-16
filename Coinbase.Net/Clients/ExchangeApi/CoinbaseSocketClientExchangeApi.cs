using Coinbase.Net.Clients.MessageHandlers;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.ExchangeApi
{
    /// <summary>
    /// Client providing access to the Coinbase Exchange websocket Api
    /// </summary>
    internal partial class CoinbaseSocketClientExchangeApi : SocketApiClient, ICoinbaseSocketClientExchangeApi
    {
        #region fields
        private static readonly MessagePath _typePath = MessagePath.Get().Property("type");
        private static readonly MessagePath _symbolPath = MessagePath.Get().Property("product_id");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CoinbaseSocketClientExchangeApi(ILogger logger, CoinbaseSocketOptions options) :
            base(logger, options.Environment.SocketClientPublicExchangeApiAddress!, options, options.AdvancedTradeOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new CoinbaseSocketExchangeMessageHandler();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExHeartbeat>> onMessage, CancellationToken ct = default)
            => SubscribeToHeartbeatUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExHeartbeat>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseExHeartbeat>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<CoinbaseExHeartbeat>(CoinbaseExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("heartbeat")
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Timestamp)
                    );
            });

            var subscription = new CoinbaseExSubscription<CoinbaseExHeartbeat>(this, _logger, "heartbeat", "heartbeat", symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToExchangeInfoUpdatesAsync(Action<DataEvent<CoinbaseExchangeInfo>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseExchangeInfo>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<CoinbaseExchangeInfo>(CoinbaseExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("status")
                    );
            });

            var subscription = new CoinbaseExSubscription<CoinbaseExchangeInfo>(this, _logger, "status", "status", null, internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default)
            => SubscribeToTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseExTicker>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<CoinbaseExTicker>(CoinbaseExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("ticker")
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Timestamp)
                    );
            });

            var subscription = new CoinbaseExSubscription<CoinbaseExTicker>(this, _logger, "ticker", "ticker", symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default)
            => SubscribeToBatchedTickerUpdatesAsync([symbol], onMessage, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseExTicker>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<CoinbaseExTicker>(CoinbaseExchange.ExchangeName, data, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId("ticker")
                        .WithSymbol(data.Symbol)
                        .WithDataTimestamp(data.Timestamp)
                    );
            });

            var subscription = new CoinbaseExSubscription<CoinbaseExTicker>(this, _logger, "ticker_batch", "ticker", symbols.ToArray(), internalHandler, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseExBookSnapshot>> onSnapshot, Action<DataEvent<CoinbaseExBookUpdate>> onUpdate, CancellationToken ct = default)
            => SubscribeToOrderBookUpdatesAsync([symbol], onSnapshot, onUpdate, ct);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseExBookSnapshot>> onSnapshot, Action<DataEvent<CoinbaseExBookUpdate>> onUpdate, CancellationToken ct = default)
        {
            var subscription = new CoinbaseExOrderBookSubscription(this, _logger, symbols.ToArray(), onSnapshot, onUpdate);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var type = message.GetValue<string>(_typePath);
            var product = message.GetValue<string>(_symbolPath);

            return type + product;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CoinbaseExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
