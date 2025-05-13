using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseExchangeRatesWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CoinbaseExchangeRates Data { get; set; } = null!;
    }

    /// <summary>
    /// Exchange rates
    /// </summary>
    [SerializationModel]
    public record CoinbaseExchangeRates
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Exchange rates
        /// </summary>
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> ExchangeRates { get; set; } = null!;
    }

}
