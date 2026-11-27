using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using System.Linq;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;

namespace Coinbase.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CoinbaseExBatchTickerSubscription<T> : Subscription
    {
        private readonly Action<DateTime, string?, T> _handler;
        private readonly string[] _symbols;
        private readonly SocketApiClient _client;

        public override string[] SubscriptionIdentifiers { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseExBatchTickerSubscription(SocketApiClient client, ILogger logger, string[] symbols, Action<DateTime, string?, T> handler) : base(logger, false)
        {
            _handler = handler;
            _client = client;
            _symbols = symbols.ToArray();

            Topic = "batch_ticker";

            IndividualSubscriptionCount = symbols.Length;
            SubscriptionIdentifiers = symbols.Select(x => $"batch_ticker{x}").ToArray();
            MessageRouter = MessageRouter.CreateForEvent<T>("ticker", symbols, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = ["ticker_batch"],
            Type = "subscribe",
            Symbols = _symbols,
        }, Authenticated, channelIdentifier: "ticker_1000");

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = ["ticker_batch"],
            Type = "unsubscribe",
            Symbols = _symbols,
        }, Authenticated, channelIdentifier: "ticker_1000");

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }

    }
}
