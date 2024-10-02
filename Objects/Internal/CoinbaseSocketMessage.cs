using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal class CoinbaseSocketMessage
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("sequence_num")]
        public long SequenceNumber { get; set; }
    }

    internal class CoinbaseSocketMessage<T> : CoinbaseSocketMessage
    {
        [JsonPropertyName("events")]
        public IEnumerable<T> Events { get; set; }
    }
}
