using Coinbase.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// User data update
    /// </summary>
    public record CoinbaseUserUpdate : CoinbaseSocketEvent
    {
        /// <summary>
        /// Order data
        /// </summary>
        [JsonPropertyName("orders")]
        public IEnumerable<CoinbaseOrderUpdate> Orders { get; set; } = Array.Empty<CoinbaseOrderUpdate>();
        /// <summary>
        /// Position data
        /// </summary>
        [JsonPropertyName("positions")]
        public CoinbasePositionsUpdates PositionInfo { get; set; } = null!;
    }

    /// <summary>
    /// Positions data
    /// </summary>
    public record CoinbasePositionsUpdates
    {
        /// <summary>
        /// Perpetual futures positions
        /// </summary>
        [JsonPropertyName("perpetual_futures_positions")]
        public IEnumerable<CoinbasePerpetualPositionUpdate> PerpetualPositions { get; set; } = Array.Empty<CoinbasePerpetualPositionUpdate>();
        /// <summary>
        /// Expiring futures positions
        /// </summary>
        [JsonPropertyName("expiring_futures_positions")]
        public IEnumerable<CoinbaseExpiringPosition> ExpiringPositions { get; set; } = Array.Empty<CoinbaseExpiringPosition>();
    }
}
