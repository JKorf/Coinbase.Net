using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseFuturesPositionsWrapper
    {
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public IEnumerable<CoinbaseFuturesPosition> Positions { get; set; } = null!;
    }

    internal record CoinbaseFuturesPositionWrapper
    {
        /// <summary>
        /// Position
        /// </summary>
        [JsonPropertyName("position")]
        public CoinbaseFuturesPosition Position { get; set; } = null!;
    }

    /// <summary>
    /// Position info
    /// </summary>
    public record CoinbaseFuturesPosition
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Expiration time
        /// </summary>
        [JsonPropertyName("expiration_time")]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Number of contracts
        /// </summary>
        [JsonPropertyName("number_of_contracts")]
        public int NumberOfContracts { get; set; }
        /// <summary>
        /// Current price
        /// </summary>
        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("avg_entry_price")]
        public decimal AverageEntryPrice { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Daily realized profit and loss
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public decimal DailyRealizedPnl { get; set; }
    }


}
