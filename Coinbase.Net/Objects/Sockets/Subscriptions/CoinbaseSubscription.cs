using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Clients;
using System.Linq;
using CryptoExchange.Net.Sockets.Default;

namespace Coinbase.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CoinbaseSubscription<T> : Subscription where T: CoinbaseSocketEvent
    {
        private readonly Action<DateTime, string?, CoinbaseSocketMessage<T>> _handler;
        private readonly string _channel;
        private readonly string[]? _symbols;
        private readonly SocketApiClient _client;

        private HashSet<string> _usdcNotReplacing = new HashSet<string>
        {
            "USDT-USDC",
            "EURC-USDC",
            "XSGD-USDC",
            "AUDD-USDC",
        };

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseSubscription(SocketApiClient client, ILogger logger, string channel, string channelIdentifier, string[]? symbols, Action<DateTime, string?, CoinbaseSocketMessage<T>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _channel = channel;
            _client = client;
            _symbols = symbols?.Select(x => !_usdcNotReplacing.Contains(x) ? x.Replace("-USDC", "-USD") : x).ToArray();

            IndividualSubscriptionCount = symbols?.Length ?? 1;

            if (_symbols?.Length > 0)
                MessageMatcher = MessageMatcher.Create(_symbols.Select(x => new MessageHandlerLink<CoinbaseSocketMessage<T>>(channelIdentifier + "-" + x, DoHandleMessage)).ToArray());
            else
                MessageMatcher = MessageMatcher.Create<CoinbaseSocketMessage<T>>(channelIdentifier, DoHandleMessage);

            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<CoinbaseSocketMessage<T>>(channelIdentifier, _symbols, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new CoinbaseSubscriptionQuery(new CoinbaseSocketRequest
        {
            Channel = _channel,
            Type = "subscribe",
            Symbols = _symbols,
            Jwt = Authenticated ? ((CoinbaseAuthenticationProvider)_client.AuthenticationProvider!).GenerateToken(DateTime.UtcNow.AddSeconds(-5), null) : null
        }, Authenticated);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new CoinbaseQuery<CoinbaseSocketMessage>(new CoinbaseSocketRequest
        {
            Channel = _channel,
            Type = "unsubscribe",
            Symbols = _symbols,
            Jwt = Authenticated ? ((CoinbaseAuthenticationProvider)_client.AuthenticationProvider!).GenerateToken(DateTime.UtcNow.AddSeconds(-5), null) : null
        }, Authenticated);

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseSocketMessage<T> message)
        {
            if (message.SequenceNumber != 0)
                connection.UpdateSequenceNumber(message.SequenceNumber);

            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }

    }
}
