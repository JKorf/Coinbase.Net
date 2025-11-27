using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
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

            MessageMatcher = MessageMatcher.Create(
                new MessageHandlerLink<CoinbaseExSubscriptionsUpdate>("subscriptions", HandleMessage),
                new MessageHandlerLink<CoinbaseExError>("error", HandleError));

            MessageRouter = MessageRouter.Create(
                new MessageRoute<CoinbaseExSubscriptionsUpdate>("subscriptions", (string?)null, HandleMessage),
                new MessageRoute<CoinbaseExError>("error", (string?)null, HandleError));
        }

        public override bool PreCheckMessage(SocketConnection connection, object message)
        {
            if (message is not CoinbaseExSubscriptionsUpdate messageData)
                return true;

            var channel = messageData.Subscriptions.SingleOrDefault(x => x.Name == _channel);
            if (channel == null)
                return false;

            if (_symbols != null && _symbols.Any(x => !channel.Symbols.Contains(x)))
                return false;

            return true;
        }

        public CallResult<CoinbaseExError> HandleError(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExError message)
        {
            return new CallResult<CoinbaseExError>(message, originalData, new ServerError(new ErrorInfo(ErrorType.UnknownSymbol, message.Reason)));
        }

        public CallResult<CoinbaseExSubscriptionsUpdate> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, CoinbaseExSubscriptionsUpdate message)
        {
            return new CallResult<CoinbaseExSubscriptionsUpdate>(message, originalData, null);
        }
    }
}
