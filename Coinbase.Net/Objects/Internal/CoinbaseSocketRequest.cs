using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseSocketRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("product_ids"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string[]? Symbols { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;
        [JsonPropertyName("jwt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Jwt { get; set; }
    }
}
