using CryptoExchange.Net.Sockets;
using Coinbase.Net.Objects.Internal;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseExQuery<T> : Query<T>
    {
        public CoinbaseExQuery(CoinbaseExSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            MessageMatcher = MessageMatcher.Create<T>("subscriptions");
            MessageRouter = MessageRouter.CreateWithoutHandler<T>("subscriptions");
        }
    }
}
