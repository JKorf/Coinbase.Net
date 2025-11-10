using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseExSocketRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("product_ids"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[]? Symbols { get; set; }
        [JsonPropertyName("channels")]
        public string[] Channels { get; set; } = [];
    }
}
