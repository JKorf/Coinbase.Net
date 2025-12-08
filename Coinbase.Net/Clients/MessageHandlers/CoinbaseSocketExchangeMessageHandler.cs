using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Text.Json;

namespace Coinbase.Net.Clients.MessageHandlers
{
    internal class CoinbaseSocketExchangeMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CoinbaseExchange._serializerContext);

        public CoinbaseSocketExchangeMessageHandler()
        {
            AddTopicMapping<CoinbaseExTicker>(x => x.Symbol);
            AddTopicMapping<CoinbaseExHeartbeat>(x => x.Symbol);
            AddTopicMapping<CoinbaseExBookSnapshot>(x => x.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("type")
                ],
                TypeIdentifierCallback = x => x.FieldValue("type")!
            },

        ];
    }
}
