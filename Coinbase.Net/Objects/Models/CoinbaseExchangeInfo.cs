using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record CoinbaseExchangeInfo
    {
        /// <summary>
        /// Symbol info
        /// </summary>
        [JsonPropertyName("products")]
        public CoinbaseExSymbol[] Symbols { get; set; } = [];
        /// <summary>
        /// Asset info
        /// </summary>
        [JsonPropertyName("currencies")]
        public CoinbaseExAsset[] Assets { get; set; } = [];
    }
}
