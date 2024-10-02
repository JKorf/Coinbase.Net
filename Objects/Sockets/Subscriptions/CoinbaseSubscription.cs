using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Internal;

namespace Coinbase.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CoinbaseSubscription<T> : Subscription<CoinbaseSocketMessage, CoinbaseSocketMessage>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly Action<DataEvent<IEnumerable<T>>> _handler;
        private readonly string _channel;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(CoinbaseSocketMessage<T>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="topics"></param>
        /// <param name="handler"></param>
        /// <param name="auth"></param>
        public CoinbaseSubscription(ILogger logger, string channel, string[] topics, Action<DataEvent<IEnumerable<T>>> handler, bool auth) : base(logger, auth)
        {
            _handler = handler;
            _channel = channel;
            ListenerIdentifiers = new HashSet<string>() { channel };
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection) => new CoinbaseQuery<CoinbaseSocketMessage>(new CoinbaseSocketRequest
        {
            Channel = _channel,
            Type = "subscribe",
        }, Authenticated);

        /// <inheritdoc />
        public override Query? GetUnsubQuery() => new CoinbaseQuery<CoinbaseSocketMessage>(new CoinbaseSocketRequest
        {
            Channel = _channel,
            Type = "unsubscribe",
        }, Authenticated);

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (CoinbaseSocketMessage<T>)message.Data;
            _handler.Invoke(message.As(data.Events, data.Channel, null, SocketUpdateType.Update));
            return new CallResult(null);
        }
    }
}
