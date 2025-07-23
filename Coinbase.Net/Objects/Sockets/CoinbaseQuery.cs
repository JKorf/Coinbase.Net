using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseQuery<T> : Query<T>
    {
        public CoinbaseQuery(CoinbaseSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<T>("subscriptions");
        }
    }
}
