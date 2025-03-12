using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using Coinbase.Net.Objects.Internal;
using System.Collections.Generic;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using CryptoExchange.Net;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Client providing access to the Coinbase websocket Api
    /// </summary>
    internal partial class CoinbaseSocketClientAdvancedTradeApi : SocketApiClient, ICoinbaseSocketClientAdvancedTradeApi
    {
        #region fields
        private static readonly MessagePath _channelPath = MessagePath.Get().Property("channel");
        private static readonly MessagePath _tradesSymbolPath = MessagePath.Get().Property("events").Index(0).Property("trades").Index(0).Property("product_id");
        private static readonly MessagePath _klinesSymbolPath = MessagePath.Get().Property("events").Index(0).Property("candles").Index(0).Property("product_id");
        private static readonly MessagePath _tickersSymbolPath = MessagePath.Get().Property("events").Index(0).Property("tickers").Index(0).Property("product_id");
        private static readonly MessagePath _symbolsSymbolPath = MessagePath.Get().Property("events").Index(0).Property("products").Index(0).Property("id");
        private static readonly MessagePath _bookSymbolPath = MessagePath.Get().Property("events").Index(0).Property("product_id");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CoinbaseSocketClientAdvancedTradeApi(ILogger logger, CoinbaseSocketOptions options) :
            base(logger, options.Environment.SocketClientPublicAddress!, options, options.AdvancedTradeOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(CoinbaseExchange.SerializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CoinbaseExchange.SerializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToHeartbeatUpdatesAsync(Action<DataEvent<CoinbaseHeartbeat>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseHeartbeat>(this, _logger, "heartbeats", "heartbeats", null, x => onMessage(x.As(x.Data.First())), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseTrade[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseTradeEvent>(this, _logger, "market_trades", "market_trades", symbols.ToArray(), x => onMessage(
                x.As(x.Data.First().Trades)
                .WithSymbol(x.Data.First().Trades.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamKline[]>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseKlineEvent>(this, _logger, "candles", "candles", symbols.ToArray(), x => onMessage(
                x.As(x.Data.First().Klines)
                .WithSymbol(x.Data.First().Klines.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseTicker>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseTickerEvent>(this, _logger, "ticker", "ticker", symbols.ToArray(), x => onMessage(
                x.As(x.Data.First().Tickers.First())
                .WithSymbol(x.Data.First().Tickers.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(string symbol, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToBatchedTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBatchedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseBatchTicker>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseBatchTickerEvent>(this, _logger, "ticker_batch", "ticker_batch", symbols.ToArray(), x => onMessage(
                x.As(x.Data.First().Tickers.First())
                .WithSymbol(x.Data.First().Tickers.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(string symbol, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default)
            => await SubscribeToSymbolUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseStreamSymbol>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseSymbolEvent>(this, _logger, "status", "status", symbols.ToArray(), x => onMessage(
                x.As(x.Data.First().Symbols.First())
                .WithSymbol(x.Data.First().Symbols.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<CoinbaseOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseOrderBookEvent>(this, _logger, "level2", "l2_data", symbols.ToArray(), x => onMessage(
                x.As(new CoinbaseOrderBookUpdate
                {
                    Asks = x.Data.First().Book.Where(a => a.Side == Enums.OrderSide.Sell).ToArray(),
                    Bids = x.Data.First().Book.Where(a => a.Side == Enums.OrderSide.Buy).ToArray()
                })
                .WithSymbol(x.Data.First().Symbol)), AuthenticationProvider != null);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserUpdatesAsync(Action<DataEvent<CoinbaseUserUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseUserUpdate>(this, _logger, "user", "user", null, x => onMessage(x.As(x.Data.First())), true);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFuturesBalanceUpdatesAsync(Action<DataEvent<CoinbaseFuturesBalance>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CoinbaseSubscription<CoinbaseFuturesBalanceUpdate>(this, _logger, "futures_balance_summary", "futures_balance_summary", null, x => onMessage(x.As(x.Data.First().BalanceSummary)), true);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var channel = message.GetValue<string>(_channelPath);
            if (channel == null)
                return null;

            if (channel.Equals("market_trades", StringComparison.Ordinal))
                channel += "-" + message.GetValue<string>(_tradesSymbolPath);
            if (channel.Equals("candles", StringComparison.Ordinal))
                channel += "-" + message.GetValue<string>(_klinesSymbolPath);
            if (channel.Equals("ticker", StringComparison.Ordinal)
                || channel.Equals("ticker_batch", StringComparison.Ordinal))
                channel += "-" + message.GetValue<string>(_tickersSymbolPath);
            if (channel.Equals("status", StringComparison.Ordinal))
                channel += "-" + message.GetValue<string>(_symbolsSymbolPath);
            if (channel.Equals("l2_data", StringComparison.Ordinal))
                channel += "-" + message.GetValue<string>(_bookSymbolPath);

            return channel;
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CoinbaseExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);
    }
}
