using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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
