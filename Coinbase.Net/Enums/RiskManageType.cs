using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Risk management type
    /// </summary>
    public enum RiskManageType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_RISK_MANAGEMENT_TYPE")]
        Unknown,
        /// <summary>
        /// Managed by FCM
        /// </summary>
        [Map("MANAGED_BY_FCM")]
        ManagedByFcm,
        /// <summary>
        /// Management by venue
        /// </summary>
        [Map("MANAGED_BY_VENUE")]
        ManagedByVenue
    }
}
