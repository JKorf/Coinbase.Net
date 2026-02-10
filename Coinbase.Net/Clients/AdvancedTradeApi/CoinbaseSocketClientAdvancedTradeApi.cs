using Coinbase.Net.Clients.MessageHandlers;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Objects.Internal;
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
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Client providing access to the Coinbase websocket Api
    /// </summary>
    internal partial class CoinbaseSocketClientAdvancedTradeApi : SocketApiClient, ICoinbaseSocketClientAdvancedTradeApi
    {
        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CoinbaseSocketClientAdvancedTradeApi(ILogger logger, CoinbaseSocketOptions options) :
            base(logger, options.Environment.SocketClientPublicAddress!, options, options.AdvancedTradeOptions)
        {
            EnforceSequenceNumbers = true;
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new CoinbaseSocketAdvancedTradeMessageConverter();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseHeartbeat>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseHeartbeat>(Exchange, data.Events.First(), receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseHeartbeat>(this, _logger, "heartbeats", "heartbeats", null, internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseTradeEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                // Won't know the symbol this message was for..
                if (data.Events.First().Trades.Length == 0)
                    return;

                onMessage(
                    new DataEvent<CoinbaseTrade[]>(Exchange, data.Events.First().Trades, receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Trades.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseTradeEvent>(this, _logger, "market_trades", "market_trades", symbols.ToArray(), internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseKlineEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseStreamKline[]>(Exchange, data.Events.First().Klines, receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Klines.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseKlineEvent>(this, _logger, "candles", "candles", symbols.ToArray(), internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseTickerEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseTicker>(Exchange, data.Events.First().Tickers.First(), receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Tickers.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseTickerEvent>(this, _logger, "ticker", "ticker", symbols.ToArray(), internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToBatchedTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseBatchTickerEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseBatchTicker>(Exchange, data.Events.First().Tickers.First(), receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Tickers.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseBatchTickerEvent>(this, _logger, "ticker_batch", "ticker_batch", symbols.ToArray(), internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default)
            => await SubscribeToSymbolUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseSymbolEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseStreamSymbol>(Exchange, data.Events.First().Symbols.First(), receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Symbols.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseSymbolEvent>(this, _logger, "status", "status", symbols.ToArray(), internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var symbolArray = symbols.ToArray();
            if (symbolArray.Length > 55)
                return new CallResult<UpdateSubscription>(new ServerError(new ErrorInfo(ErrorType.InvalidParameter, "API doesn't allow more than 55 orderbook subscription on a single socket, split the call in multiple batches")));

            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseOrderBookEvent>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                var book = new CoinbaseOrderBookUpdate
                {
                    Asks = data.Events.First().Book.Where(a => a.Side == Enums.OrderSide.Sell).ToArray(),
                    Bids = data.Events.First().Book.Where(a => a.Side == Enums.OrderSide.Buy).ToArray()
                };

                onMessage(
                    new DataEvent<CoinbaseOrderBookUpdate>(Exchange, book, receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithSymbol(data.Events.First().Symbol)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseOrderBookEvent>(this, _logger, "level2", "l2_data", symbolArray, internalHandler, AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(Action<DataEvent<CoinbaseUserUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseUserUpdate>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseUserUpdate>(Exchange, data.Events.First(), receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseUserUpdate>(this, _logger, "user", "user", null, internalHandler, true);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFuturesBalanceUpdatesAsync(Action<DataEvent<CoinbaseFuturesBalance>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, CoinbaseSocketMessage<CoinbaseFuturesBalanceUpdate>>((receiveTime, originalData, data) =>
            {
                var eventType = data.Events.First().EventType.Equals("snapshot") ? SocketUpdateType.Snapshot : SocketUpdateType.Update;
                if (eventType == SocketUpdateType.Update)
                    UpdateTimeOffset(data.Timestamp);

                onMessage(
                    new DataEvent<CoinbaseFuturesBalance>(Exchange, data.Events.First().BalanceSummary, receiveTime, originalData)
                        .WithUpdateType(eventType)
                        .WithStreamId(data.Channel)
                        .WithDataTimestamp(data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.SequenceNumber)
                    );
            });

            var subscription = new CoinbaseSubscription<CoinbaseFuturesBalanceUpdate>(this, _logger, "futures_balance_summary", "futures_balance_summary", null, internalHandler, true);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        protected override bool HandleUnhandledMessage(SocketConnection connection, string typeIdentifier, ReadOnlySpan<byte> data)
        {
            // If a snapshot message is send for a symbol subscription without any data we don't know which symbol it was for so we can't process,
            // but we still need to update the sequence number
            var doc = JsonDocument.Parse(data.ToArray());
            if (doc.RootElement.ValueKind != JsonValueKind.Object)
                return false;

            // Sequence number is in second to last field
            if (!doc.RootElement.TryGetProperty("sequence_num", out var seqProp))
                return false;

            try
            {
                var sequenceValue = seqProp.GetInt64();
                _logger.LogDebug($"Setting connection sequence number to {sequenceValue} for unhandled message");
                connection.UpdateSequenceNumber(sequenceValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CoinbaseExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
