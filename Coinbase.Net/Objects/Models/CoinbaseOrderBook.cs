using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseOrderBookWrapper
    {
        [JsonPropertyName("pricebook")]
        public CoinbaseOrderBook Book { get; set; } = null!;
    }

    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderBook
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asks</c>"] Asks list
        /// </summary>
        [JsonPropertyName("asks")]
        public CoinbaseOrderBookEntry[] Asks { get; set; } = Array.Empty<CoinbaseOrderBookEntry>();
        /// <summary>
        /// ["<c>bids</c>"] Bids list
        /// </summary>
        [JsonPropertyName("bids")]
        public CoinbaseOrderBookEntry[] Bids { get; set; } = Array.Empty<CoinbaseOrderBookEntry>();

        /// <summary>
        /// ["<c>time</c>"] Time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
    }
}
