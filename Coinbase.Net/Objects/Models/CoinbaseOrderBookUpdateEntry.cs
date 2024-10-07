using Coinbase.Net.Enums;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    public record CoinbaseOrderBookUpdate
    {
        /// <summary>
        /// List of bids
        /// </summary>
        public IEnumerable<CoinbaseOrderBookUpdateEntry> Bids { get; set; } = Array.Empty<CoinbaseOrderBookUpdateEntry>();
        /// <summary>
        /// List of asks
        /// </summary>
        public IEnumerable<CoinbaseOrderBookUpdateEntry> Asks { get; set; } = Array.Empty<CoinbaseOrderBookUpdateEntry>();
    }

    /// <summary>
    /// Order book update
    /// </summary>
    public record CoinbaseOrderBookUpdateEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("event_time")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price_level")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("new_quantity")]
        public decimal Quantity { get; set; }
    }
}
