using Coinbase.Net.Converters;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order configuration
    /// </summary>
    [JsonConverter(typeof(OrderConfigurationConverter))]
    public record CoinbaseOrderConfiguration
    {
        /// <summary>
        /// Type of order
        /// </summary>
        public NewOrderType OrderType { get; set; }
        /// <summary>
        /// Order quantity in base asset
        /// </summary>
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Order quantity in quote asset
        /// </summary>
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Post only flag
        /// </summary>
        public bool? PostOnly { get; set; }
        /// <summary>
        /// Time the order is canceled
        /// </summary>
        public DateTime? CancelTime { get; set; }
        /// <summary>
        /// Start time for a Twap order
        /// </summary>
        public DateTime? TwapStartTime { get; set; }
        /// <summary>
        /// End time for a Twap order
        /// </summary>
        public DateTime? TwapEndTime { get; set; }
        /// <summary>
        /// Stop order trigger direction
        /// </summary>
        public StopDirection? StopDirection { get; set; }
        /// <summary>
        /// Stop order trigger price
        /// </summary>
        public decimal? StopPrice { get; set; }
    }
}
