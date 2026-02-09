using Coinbase.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System.Linq;
using System.Text.Json;

namespace Coinbase.Net.Clients.MessageHandlers
{
    internal class CoinbaseSocketAdvancedTradeMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CoinbaseExchange._serializerContext);

        public CoinbaseSocketAdvancedTradeMessageConverter()
        {
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseTradeEvent>>(x => x.Events.First().Trades.First().Symbol);
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseKlineEvent>>(x => x.Events.First().Klines.First().Symbol);
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseTickerEvent>>(x => x.Events.First().Tickers.First().Symbol);
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseBatchTickerEvent>>(x => x.Events.First().Tickers.First().Symbol);
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseOrderBookEvent>>(x => x.Events.First().Symbol);  
            AddTopicMapping<CoinbaseSocketMessage<CoinbaseSymbolEvent>>(x => x.Events.First().Symbols.First().Symbol);  
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel")
                ],
                TypeIdentifierCallback = x => x.FieldValue("channel")!,
            }
        ];
    }
}
