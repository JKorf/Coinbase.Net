using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbasePriceWrapper
    {
        [JsonPropertyName("data")]
        public IEnumerable<CoinbasePrice> Prices { get; set; } = Array.Empty<CoinbasePrice>();
    }

    /// <summary>
    /// Price info
    /// </summary>
    public record CoinbasePrice
    {
        /// <summary>
        /// The price
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Price { get; set; }
        /// <summary>
        /// The currency in which the price is
        /// </summary>
        [JsonPropertyName("base")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The currency in which the price is
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
    }
}
