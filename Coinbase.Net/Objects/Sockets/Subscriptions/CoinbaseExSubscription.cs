using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using System.Linq;

namespace Coinbase.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CoinbaseExSubscription<T> : Subscription
    {
        private readonly Action<DateTime, string?, T> _handler;
        private readonly string _channel;
        private readonly string[]? _symbols;
        private readonly SocketApiClient _client;

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseExSubscription(SocketApiClient client, ILogger logger, string channel, string channelIdentifier, string[]? symbols, Action<DateTime, string?, T> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _channel = channel;
            _client = client;
            _symbols = symbols?.ToArray();

            if (_symbols?.Length > 0)
            {
                MessageMatcher = MessageMatcher.Create(_symbols.Select(x => new MessageHandlerLink<T>(channelIdentifier + x, DoHandleMessage)).ToArray());
                //MessageRouter = MessageRouter.Create(_symbols.Select(x => new MessageRoute<T>(channelIdentifier + x, (string?)null, DoHandleMessage)).ToArray());
            }
            else
            {
                MessageMatcher = MessageMatcher.Create<T>(channelIdentifier, DoHandleMessage);
                //MessageRouter = MessageRouter.Create<T>(channelIdentifier, DoHandleMessage);
            }

            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<T>(channelIdentifier, symbols, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = [_channel],
            Type = "subscribe",
            Symbols = _symbols,
        }, Authenticated);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = [_channel],
            Type = "unsubscribe",
            Symbols = _symbols,
        }, Authenticated);

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }

    }
}
