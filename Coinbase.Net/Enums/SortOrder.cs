using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// Descending
        /// </summary>
        [Map("desc")]
        Descending
    }
}
