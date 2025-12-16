using Coinbase.Net.Objects.Internal;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coinbase.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class CoinbaseExOrderBookSubscription : Subscription
    {
        private readonly Action<DataEvent<CoinbaseExBookSnapshot>> _snapshotHandler;
        private readonly Action<DataEvent<CoinbaseExBookUpdate>> _updateHandler;
        private readonly string[]? _symbols;
        private readonly SocketApiClient _client;

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseExOrderBookSubscription(SocketApiClient client, ILogger logger, string[] symbols, Action<DataEvent<CoinbaseExBookSnapshot>> snapshotHandler, Action<DataEvent<CoinbaseExBookUpdate>> updateHandler) : base(logger, false)
        {
            _snapshotHandler = snapshotHandler;
            _updateHandler = updateHandler;
            _client = client;
            _symbols = symbols.ToArray();

            IndividualSubscriptionCount = symbols.Length;

            MessageMatcher = MessageMatcher.Create(_symbols.SelectMany(x => 
                 new MessageHandlerLink[] { new MessageHandlerLink<CoinbaseExBookSnapshot>("snapshot" + x, DoHandleMessage),
                 new MessageHandlerLink<CoinbaseExBookUpdate>("l2update" + x, DoHandleMessage)
             }).ToArray());

            var routes = new List<MessageRoute>();
            foreach(var symbol in symbols)
            {
                routes.Add(MessageRoute<CoinbaseExBookSnapshot>.CreateWithTopicFilter("snapshot", symbol, DoHandleMessage));
                routes.Add(MessageRoute<CoinbaseExBookUpdate>.CreateWithTopicFilter("l2update", symbol, DoHandleMessage));
            }
            MessageRouter = MessageRouter.Create(routes.ToArray());
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = ["level2_batch"],
            Type = "subscribe",
            Symbols = _symbols,
        }, Authenticated);

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection) => new CoinbaseExSubscriptionQuery(new CoinbaseExSocketRequest
        {
            Channels = ["level2_batch"],
            Type = "unsubscribe",
            Symbols = _symbols,
        }, Authenticated);

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExBookSnapshot message)
        {
            _snapshotHandler.Invoke(
                new DataEvent<CoinbaseExBookSnapshot>(CoinbaseExchange.ExchangeName, message, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Snapshot)
                    .WithSymbol(message.Symbol)
                );
            return CallResult.SuccessResult;
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExBookUpdate message)
        {
            _updateHandler.Invoke(
                new DataEvent<CoinbaseExBookUpdate>(CoinbaseExchange.ExchangeName, message, receiveTime, originalData)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithSymbol(message.Symbol)
                );
            return CallResult.SuccessResult;
        }
    }
}
