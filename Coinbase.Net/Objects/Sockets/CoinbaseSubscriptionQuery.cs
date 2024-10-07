using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;
using System.Linq;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseSubscriptionQuery : Query<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly string _channel;
        private readonly string[]? _symbols;

        public CoinbaseSubscriptionQuery(CoinbaseSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { "subscriptions" };
            _channel = request.Channel;
            _symbols = request.Symbols;
        }

        public override CallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>> HandleMessage(SocketConnection connection, DataEvent<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>> message)
        {
            var evnt = message.Data.Events.First();
            if (!evnt.Subscriptions.TryGetValue(_channel, out var subbed))
                return message.ToCallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>(new ServerError("Server has not aknowledged subscription"));

            if (_symbols != null && _symbols.Any(x => !subbed.Contains(x)))
                return message.ToCallResult<CoinbaseSocketMessage<CoinbaseSubscriptionsUpdate>>(new ServerError("Server has not aknowledged all symbols"));

            return message.ToCallResult();
        }
    }
}
