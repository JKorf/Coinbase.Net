using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseExQuery<T> : Query<T>
    {
        public CoinbaseExQuery(CoinbaseExSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<T>("subscriptions");
            MessageRouter = MessageRouter.Create<T>("subscriptions");
        }
    }
}
