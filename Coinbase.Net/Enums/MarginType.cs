using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    public enum MarginType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("MARGIN_TYPE_UNSPECIFIED", "UNKNOWN_MARGIN_TYPE")]
        Unspecified,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("MARGIN_TYPE_CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("MARGIN_TYPE_ISOLATED")]
        Isolated
    }
}
