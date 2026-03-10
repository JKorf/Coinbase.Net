using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Trade sort order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeSortBy>))]
    public enum TradeSortBy
    {
        /// <summary>
        /// ["<c>UNKNOWN_SORT_BY</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_SORT_BY")]
        Unknown,
        /// <summary>
        /// ["<c>PRICE</c>"] Trade price
        /// </summary>
        [Map("PRICE")]
        Price,
        /// <summary>
        /// ["<c>TRADE_TIME</c>"] Trade time
        /// </summary>
        [Map("TRADE_TIME")]
        TradeTime
    }
}
