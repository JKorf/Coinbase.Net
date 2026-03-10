using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>BUY</c>"] Buy
        /// </summary>
        [Map("BUY", "buy", "bid")]
        Buy,
        /// <summary>
        /// ["<c>SELL</c>"] Sell
        /// </summary>
        [Map("SELL", "sell", "ask", "offer")]
        Sell,
        /// <summary>
        /// ["<c>UNKNOWN_ORDER_SIDE</c>"] Unkown, only for data mapping
        /// </summary>
        [Map("UNKNOWN_ORDER_SIDE")]
        Unknown
    }
}
