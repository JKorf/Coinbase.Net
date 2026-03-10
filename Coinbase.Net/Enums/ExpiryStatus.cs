using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status of expiry
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ExpiryStatus>))]
    public enum ExpiryStatus
    {
        /// <summary>
        /// ["<c>UNKNOWN_EXPIRING_CONTRACT_STATUS</c>"] Unknown status
        /// </summary>
        [Map("UNKNOWN_EXPIRING_CONTRACT_STATUS")]
        Unknown,
        /// <summary>
        /// ["<c>STATUS_UNEXPIRED</c>"] Unexpired status
        /// </summary>
        [Map("STATUS_UNEXPIRED")]
        Unexpired,
        /// <summary>
        /// ["<c>STATUS_EXPIRED</c>"] Expired contract
        /// </summary>
        [Map("STATUS_EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>STATUS_ALL</c>"] All statuses
        /// </summary>
        [Map("STATUS_ALL")]
        All
    }
}
