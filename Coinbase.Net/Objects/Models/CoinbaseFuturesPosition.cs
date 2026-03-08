using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseFuturesPositionsWrapper
    {
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public CoinbaseFuturesPosition[] Positions { get; set; } = null!;
    }

    [SerializationModel]
    internal record CoinbaseFuturesPositionWrapper
    {
        /// <summary>
        /// ["<c>position</c>"] Position
        /// </summary>
        [JsonPropertyName("position")]
        public CoinbaseFuturesPosition Position { get; set; } = null!;
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesPosition
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>expiration_time</c>"] Expiration time
        /// </summary>
        [JsonPropertyName("expiration_time")]
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>number_of_contracts</c>"] Number of contracts
        /// </summary>
        [JsonPropertyName("number_of_contracts")]
        public int NumberOfContracts { get; set; }
        /// <summary>
        /// ["<c>current_price</c>"] Current price
        /// </summary>
        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// ["<c>avg_entry_price</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("avg_entry_price")]
        public decimal AverageEntryPrice { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>daily_realized_pnl</c>"] Daily realized profit and loss
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public decimal DailyRealizedPnl { get; set; }
    }


}
