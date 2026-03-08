using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Expiring position info
    /// </summary>
    [SerializationModel]
    public record CoinbaseExpiringPosition
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side of the position
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>number_of_contracts</c>"] Number of contracts
        /// </summary>
        [JsonPropertyName("number_of_contracts")]
        public int NumberOfContracts { get; set; }
        /// <summary>
        /// ["<c>realized_pnl</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realized_pnl")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>entry_price</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entry_price")]
        public decimal EntryPrice { get; set; }
    }


}
