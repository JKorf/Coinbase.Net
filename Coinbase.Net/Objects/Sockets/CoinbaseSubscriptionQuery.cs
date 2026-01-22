using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using System.Linq;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseSubscriptionQuery : Query<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>
    {
        private readonly string _channel;
        private readonly string[]? _symbols;

        public CoinbaseSubscriptionQuery(CoinbaseSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _channel = request.Channel;
            _symbols = request.Symbols;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>("subscriptions", HandleMessage, true);

            RequestTimeout = TimeSpan.FromSeconds(5);
        }

        public CallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>? HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate> message)
        {
            if (message.SequenceNumber != 0)
                connection.UpdateSequenceNumber(message.SequenceNumber);

            var evnt = message.Events.First();
            if (!evnt.Subscriptions.TryGetValue(_channel, out var subbed))
                return null;

            if (_symbols != null && _symbols.Any(x => !subbed.Contains(x)))
                return null;

            return new CallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>(message, originalData, null);
        }
    }
}
