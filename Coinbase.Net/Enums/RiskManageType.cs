using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Risk management type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<RiskManageType>))]
    public enum RiskManageType
    {
        /// <summary>
        /// ["<c>UNKNOWN_RISK_MANAGEMENT_TYPE</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_RISK_MANAGEMENT_TYPE")]
        Unknown,
        /// <summary>
        /// ["<c>MANAGED_BY_FCM</c>"] Managed by FCM
        /// </summary>
        [Map("MANAGED_BY_FCM")]
        ManagedByFcm,
        /// <summary>
        /// ["<c>MANAGED_BY_VENUE</c>"] Management by venue
        /// </summary>
        [Map("MANAGED_BY_VENUE")]
        ManagedByVenue
    }
}
