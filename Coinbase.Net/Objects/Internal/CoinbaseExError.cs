using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseExError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
    }
}
