using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseOrderBookEvent : CoinbaseSocketEvent
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Book data
        /// </summary>
        [JsonPropertyName("updates")]
        public IEnumerable<CoinbaseOrderBookUpdateEntry> Book { get; set; } = Array.Empty<CoinbaseOrderBookUpdateEntry>();
    }
}
