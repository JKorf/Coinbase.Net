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
        /// ["<c>ACCOUNT_TYPE_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("ACCOUNT_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>ACCOUNT_TYPE_CRYPTO</c>"] Crypto account
        /// </summary>
        [Map("ACCOUNT_TYPE_CRYPTO")]
        Crypto,
        /// <summary>
        /// ["<c>ACCOUNT_TYPE_FIAT</c>"] Fiat account
        /// </summary>
        [Map("ACCOUNT_TYPE_FIAT")]
        Fiat,
        /// <summary>
        /// ["<c>ACCOUNT_TYPE_VAULT</c>"] Vault account
        /// </summary>
        [Map("ACCOUNT_TYPE_VAULT")]
        Vault,
        /// <summary>
        /// ["<c>ACCOUNT_TYPE_PERP_FUTURES</c>"] Perpetual futures account
        /// </summary>
        [Map("ACCOUNT_TYPE_PERP_FUTURES")]
        PerpetualFutures
    }
}
