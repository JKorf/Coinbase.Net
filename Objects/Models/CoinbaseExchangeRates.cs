using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
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
