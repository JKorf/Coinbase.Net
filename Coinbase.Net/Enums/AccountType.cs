using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("ACCOUNT_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Crypto account
        /// </summary>
        [Map("ACCOUNT_TYPE_CRYPTO")]
        Crypto,
        /// <summary>
        /// Fiat account
        /// </summary>
        [Map("ACCOUNT_TYPE_FIAT")]
        Fiat,
        /// <summary>
        /// Vault account
        /// </summary>
        [Map("ACCOUNT_TYPE_VAULT")]
        Vault,
        /// <summary>
        /// Perpetual futures account
        /// </summary>
        [Map("ACCOUNT_TYPE_PERP_FUTURES")]
        PerpetualFutures
    }
}
