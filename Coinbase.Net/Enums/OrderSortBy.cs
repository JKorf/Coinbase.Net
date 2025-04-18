using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSortBy>))]
    public enum OrderSortBy
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_SORT_BY")]
        Unknown,
        /// <summary>
        /// Limit price
        /// </summary>
        [Map("LIMIT_PRICE")]
        Price,
        /// <summary>
        /// Last fill time
        /// </summary>
        [Map("LAST_FILL_TIME")]
        LastFillTime
    }
}
