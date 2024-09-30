using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    public record CoinbaseTicker
    {
        /// <summary>
        /// Last trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long? LastTradeId { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Last trade quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Trade volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Rfq volume
        /// </summary>
        [JsonPropertyName("rfq_volume")]
        public decimal RfqVolume { get; set; }
        /// <summary>
        /// Conversions volume
        /// </summary>
        [JsonPropertyName("conversions_volume")]
        public decimal ConversionsVolume { get; set; }
    }


}
