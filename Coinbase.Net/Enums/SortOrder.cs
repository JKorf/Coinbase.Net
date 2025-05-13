using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SortOrder>))]
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
