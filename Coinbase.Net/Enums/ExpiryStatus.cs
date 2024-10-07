using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status of expiry
    /// </summary>
    public enum ExpiryStatus
    {
        /// <summary>
        /// Unknown status
        /// </summary>
        [Map("UNKNOWN_EXPIRING_CONTRACT_STATUS")]
        Unknown,
        /// <summary>
        /// Unexpired status
        /// </summary>
        [Map("STATUS_UNEXPIRED")]
        Unexpired,
        /// <summary>
        /// Expired contract
        /// </summary>
        [Map("STATUS_EXPIRED")]
        Expired,
        /// <summary>
        /// All statuses
        /// </summary>
        [Map("STATUS_ALL")]
        All
    }
}
