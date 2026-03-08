using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderBookUpdate
    {
        /// <summary>
        /// List of bids
        /// </summary>
        public CoinbaseOrderBookUpdateEntry[] Bids { get; set; } = Array.Empty<CoinbaseOrderBookUpdateEntry>();
        /// <summary>
        /// List of asks
        /// </summary>
        public CoinbaseOrderBookUpdateEntry[] Asks { get; set; } = Array.Empty<CoinbaseOrderBookUpdateEntry>();
    }

    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderBookUpdateEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// ["<c>event_time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("event_time")]
        public DateTime EventTime { get; set; }
        /// <summary>
        /// ["<c>price_level</c>"] Price
        /// </summary>
        [JsonPropertyName("price_level")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>new_quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("new_quantity")]
        public decimal Quantity { get; set; }
    }
}
