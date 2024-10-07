using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Batch ticker
    /// </summary>
    public record CoinbaseBatchTicker
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_24_h")]
        public decimal Volume24H { get; set; }
        /// <summary>
        /// Lowest price in last 24 hours
        /// </summary>
        [JsonPropertyName("low_24_h")]
        public decimal LowPrice24H { get; set; }
        /// <summary>
        /// Highest price in last 24 hours
        /// </summary>
        [JsonPropertyName("high_24_h")]
        public decimal HighPrice24H { get; set; }
        /// <summary>
        /// Lowest price in last 52 weeks
        /// </summary>
        [JsonPropertyName("low_52_w")]
        public decimal LowPrice52W { get; set; }
        /// <summary>
        /// Highest price in last 52 weeks
        /// </summary>
        [JsonPropertyName("high_52_w")]
        public decimal HighPrice52W { get; set; }
        /// <summary>
        /// Price change percentage in last 24 hours
        /// </summary>
        [JsonPropertyName("price_percent_chg_24_h")]
        public decimal PricePercentChange24H { get; set; }
    }

    /// <summary>
    /// Ticker info
    /// </summary>
    public record CoinbaseTicker : CoinbaseBatchTicker
    {
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("best_bid_quantity")]
        public decimal BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("best_ask_quantity")]
        public decimal BestAskQuantity { get; set; }
    }
}
