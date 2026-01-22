using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;
using System.Linq;

namespace Coinbase.Net.Objects.Sockets
{
    internal class CoinbaseExSubscriptionQuery : Query<CoinbaseExSubscriptionsUpdate>
    {
        private readonly string _channel;
        private readonly string[]? _symbols;

        public CoinbaseExSubscriptionQuery(CoinbaseExSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _channel = request.Channels.Single();
            _symbols = request.Symbols;

            MessageRouter = MessageRouter.Create(
                MessageRoute<CoinbaseExSubscriptionsUpdate>.CreateWithoutTopicFilter("subscriptions", HandleMessage, true),
                MessageRoute<CoinbaseExError>.CreateWithoutTopicFilter("error", HandleError));
        }

        public CallResult<CoinbaseExError> HandleError(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExError message)
        {
            return new CallResult<CoinbaseExError>(message, originalData, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, message.Reason)));
        }

        public CallResult<CoinbaseExSubscriptionsUpdate>? HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExSubscriptionsUpdate message)
        {
            var channel = message.Subscriptions.SingleOrDefault(x => x.Name == _channel);
            if (channel == null)
                return null;

            if (_symbols != null && _symbols.Any(x => !channel.Symbols.Contains(x)))
                return null;

            return new CallResult<CoinbaseExSubscriptionsUpdate>(message, originalData, null);
        }
    }
}
