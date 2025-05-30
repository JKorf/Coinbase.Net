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
        /// Unspecified
        /// </summary>
        [Map("ACCOUNT_PLATFORM_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Spot
        /// </summary>
        [Map("ACCOUNT_PLATFORM_CONSUMER")]
        Spot,
        /// <summary>
        /// US derivatives consumer
        /// </summary>
        [Map("ACCOUNT_PLATFORM_CFM_CONSUMER")]
        UsDerivatives,
        /// <summary>
        /// International exchange
        /// </summary>
        [Map("ACCOUNT_PLATFORM_INTX")]
        InternationalExchange
    }
}
