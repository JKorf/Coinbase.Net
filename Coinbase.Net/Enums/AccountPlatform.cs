using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Platform
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountPlatform>))]
    public enum AccountPlatform
    {
        /// <summary>
        /// ["<c>ACCOUNT_PLATFORM_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("ACCOUNT_PLATFORM_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>ACCOUNT_PLATFORM_CONSUMER</c>"] Spot
        /// </summary>
        [Map("ACCOUNT_PLATFORM_CONSUMER")]
        Spot,
        /// <summary>
        /// ["<c>ACCOUNT_PLATFORM_CFM_CONSUMER</c>"] US derivatives consumer
        /// </summary>
        [Map("ACCOUNT_PLATFORM_CFM_CONSUMER")]
        UsDerivatives,
        /// <summary>
        /// ["<c>ACCOUNT_PLATFORM_INTX</c>"] International exchange
        /// </summary>
        [Map("ACCOUNT_PLATFORM_INTX")]
        InternationalExchange
    }
}
