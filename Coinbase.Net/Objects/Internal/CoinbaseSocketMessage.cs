using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseSocketMessage
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("sequence_num")]
        public long SequenceNumber { get; set; }
    }

    [SerializationModel]
    internal record CoinbaseSocketMessage<T> : CoinbaseSocketMessage
    {
        [JsonPropertyName("events")]
        public T[] Events { get; set; } = Array.Empty<T>();
    }
}
