using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Batch ticker
    /// </summary>
    [SerializationModel]
    public record CoinbaseBatchTicker
    {
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>volume_24_h</c>"] Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_24_h")]
        public decimal? Volume24H { get; set; }
        /// <summary>
        /// ["<c>low_24_h</c>"] Lowest price in last 24 hours
        /// </summary>
        [JsonPropertyName("low_24_h")]
        public decimal? LowPrice24H { get; set; }
        /// <summary>
        /// ["<c>high_24_h</c>"] Highest price in last 24 hours
        /// </summary>
        [JsonPropertyName("high_24_h")]
        public decimal? HighPrice24H { get; set; }
        /// <summary>
        /// ["<c>low_52_w</c>"] Lowest price in last 52 weeks
        /// </summary>
        [JsonPropertyName("low_52_w")]
        public decimal? LowPrice52W { get; set; }
        /// <summary>
        /// ["<c>high_52_w</c>"] Highest price in last 52 weeks
        /// </summary>
        [JsonPropertyName("high_52_w")]
        public decimal? HighPrice52W { get; set; }
        /// <summary>
        /// ["<c>price_percent_chg_24_h</c>"] Price change percentage in last 24 hours
        /// </summary>
        [JsonPropertyName("price_percent_chg_24_h")]
        public decimal? PricePercentChange24H { get; set; }
    }

    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record CoinbaseTicker : CoinbaseBatchTicker
    {
        /// <summary>
        /// ["<c>best_bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>best_bid_quantity</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("best_bid_quantity")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>best_ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>best_ask_quantity</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("best_ask_quantity")]
        public decimal? BestAskQuantity { get; set; }
    }
}
