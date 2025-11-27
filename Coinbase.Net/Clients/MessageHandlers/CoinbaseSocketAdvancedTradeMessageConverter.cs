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
    internal class CoinbaseSocketAdvancedTradeMessageConverter : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CoinbaseExchange._serializerContext);

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("market_trades", StringComparison.Ordinal) },
                    new PropertyFieldReference("product_id") { Depth = 5 }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}-{x.FieldValue("product_id")}",
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("candles", StringComparison.Ordinal) },
                    new PropertyFieldReference("product_id") { Depth = 5 }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}-{x.FieldValue("product_id")}",
            },

            new MessageEvaluator {
                Priority = 3,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("ticker", StringComparison.Ordinal) || x.Equals("ticker_batch", StringComparison.Ordinal) },
                    new PropertyFieldReference("product_id") { Depth = 5 }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}-{x.FieldValue("product_id")}",
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("status", StringComparison.Ordinal) },
                    new PropertyFieldReference("id") { Depth = 5 }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}-{x.FieldValue("id")}",
            },

            new MessageEvaluator {
                Priority = 5,
                Fields = [
                    new PropertyFieldReference("channel") { Constraint = x => x!.Equals("l2_data", StringComparison.Ordinal) },
                    new PropertyFieldReference("product_id") { Depth = 3 }
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}-{x.FieldValue("product_id")}",
            },

            new MessageEvaluator {
                Priority = 6,
                Fields = [
                    new PropertyFieldReference("channel"),
                ],
                IdentifyMessageCallback = x => $"{x.FieldValue("channel")}",
            },
        ];
    }
}
