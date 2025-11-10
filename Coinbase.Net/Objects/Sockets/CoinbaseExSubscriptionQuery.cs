using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using Coinbase.Net.Objects.Internal;
using System.Linq;
using CryptoExchange.Net.Objects.Errors;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseExSubscriptionQuery : Query<CoinbaseSubscriptionsUpdate>
    {
        private readonly string _channel;
        private readonly string[]? _symbols;

        public CoinbaseExSubscriptionQuery(CoinbaseExSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _channel = request.Channels.Single();
            _symbols = request.Symbols;

            MessageMatcher = MessageMatcher.Create(
                new MessageHandlerLink<CoinbaseExSubscriptionsUpdate>("subscriptions", HandleMessage),
                new MessageHandlerLink<CoinbaseExError>("error", HandleError));
        }

        public override bool PreCheckMessage(SocketConnection connection, DataEvent<object> message)
        {
            if (message.Data is not CoinbaseExSubscriptionsUpdate)
                return true;

            var messageData = (CoinbaseExSubscriptionsUpdate)message.Data;
            var channel = messageData.Subscriptions.SingleOrDefault(x => x.Name == _channel);
            if (channel == null)
                return false;

            if (_symbols != null && _symbols.Any(x => !channel.Symbols.Contains(x)))
                return false;

            return true;
        }

        public CallResult<CoinbaseExError> HandleError(SocketConnection connection, DataEvent<CoinbaseExError> message)
        {
            return new CallResult<CoinbaseExError>(message.Data, message.OriginalData, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, message.Data.Reason)));
        }

        public CallResult<CoinbaseExSubscriptionsUpdate> HandleMessage(SocketConnection connection, DataEvent<CoinbaseExSubscriptionsUpdate> message)
        {
            return message.ToCallResult();
        }
    }
}
