using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal class CoinbaseSocketRequest
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("channel")]
        public string Channel { get; set; }
        [JsonPropertyName("jwt"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Jwt { get; set; }
        [JsonPropertyName("product_ids"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public IEnumerable<string>? Symbols { get; set; }
    }
}
