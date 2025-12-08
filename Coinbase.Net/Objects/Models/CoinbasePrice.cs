using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePriceWrapper
    {
        [JsonPropertyName("data")]
        public CoinbasePrice[] Prices { get; set; } = Array.Empty<CoinbasePrice>();
    }

    /// <summary>
    /// Price info
    /// </summary>
    [SerializationModel]
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
