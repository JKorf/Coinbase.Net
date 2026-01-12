using CryptoExchange.Net.Sockets;
using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets.Default;
using System;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseQuery<T> : Query<T>
    {
        public CoinbaseQuery(CoinbaseSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<T>("subscriptions");
            MessageRouter = MessageRouter.CreateWithoutHandler<T>("subscriptions");
        }

        public CallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>? HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate> message)
        {
            if (message.SequenceNumber != 0)
                connection.UpdateSequenceNumber(message.SequenceNumber);

            return new CallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>(message, originalData, null);
        }
    }
}
