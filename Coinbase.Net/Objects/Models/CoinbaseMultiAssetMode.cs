using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Multi asset mode status
    /// </summary>
    [SerializationModel]
    public record CoinbaseMultiAssetMode
    {
        /// <summary>
        /// Is multi asset collateral enabled
        /// </summary>
        [JsonPropertyName("multi_asset_collateral_enabled")]
        public bool MultiAssetCollateralEnabled { get; set; }
    }
}
