using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Multi asset mode status
    /// </summary>
    public record CoinbaseMultiAssetMode
    {
        /// <summary>
        /// Is multi asset collateral enabled
        /// </summary>
        [JsonPropertyName("multi_asset_collateral_enabled")]
        public bool MultiAssetCollateralEnabled { get; set; }
    }
}
