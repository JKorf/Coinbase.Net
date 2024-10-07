using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseSocketMessage
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("sequence_num")]
        public long SequenceNumber { get; set; }
    }

    internal record CoinbaseSocketMessage<T> : CoinbaseSocketMessage
    {
        [JsonPropertyName("events")]
        public IEnumerable<T> Events { get; set; } = Array.Empty<T>();
    }
}
