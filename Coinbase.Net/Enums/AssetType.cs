using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Type of asset
    /// </summary>
    public enum AssetType
    {
        /// <summary>
        /// Fiat asset
        /// </summary>
        [Map("fiat")]
        Fiat,
        /// <summary>
        /// Crypto asset
        /// </summary>
        [Map("crypto")]
        Crypto
    }
}
