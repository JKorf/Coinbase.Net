using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
