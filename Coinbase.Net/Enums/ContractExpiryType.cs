using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Expiry type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractExpiryType>))]
    public enum ContractExpiryType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_CONTRACT_EXPIRY_TYPE")]
        Unknown,
        /// <summary>
        /// Expiring contract
        /// </summary>
        [Map("EXPIRING")]
        Expiring,
        /// <summary>
        /// Perpetual contract
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual
    }
}
