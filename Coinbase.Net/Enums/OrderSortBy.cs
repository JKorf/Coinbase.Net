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
        /// ["<c>UNKNOWN_SORT_BY</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_SORT_BY")]
        Unknown,
        /// <summary>
        /// ["<c>LIMIT_PRICE</c>"] Limit price
        /// </summary>
        [Map("LIMIT_PRICE")]
        Price,
        /// <summary>
        /// ["<c>LAST_FILL_TIME</c>"] Last fill time
        /// </summary>
        [Map("LAST_FILL_TIME")]
        LastFillTime
    }
}
