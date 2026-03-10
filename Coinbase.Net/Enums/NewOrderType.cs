using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NewOrderType>))]
    public enum NewOrderType
    {
        /// <summary>
        /// ["<c>market_market_ioc</c>"] Market order
        /// </summary>
        [Map("market_market_ioc")]
        Market,
        /// <summary>
        /// ["<c>limit_limit_gtc</c>"] Limit order
        /// </summary>
        [Map("limit_limit_gtc")]
        Limit,
        /// <summary>
        /// ["<c>sor_limit_ioc</c>"] Limit order, will only succeed if it is possible to fill at least a part of the order, any unfilled quantity is canceled
        /// </summary>
        [Map("sor_limit_ioc")]
        LimitImmediateOrCancel,
        /// <summary>
        /// ["<c>limit_limit_gtd</c>"] Limit order, active until a certain date
        /// </summary>
        [Map("limit_limit_gtd")]
        LimitGoodTillDate,
        /// <summary>
        /// ["<c>limit_limit_fok</c>"] Limit order, will only succeed if it is to immediately and completely filled
        /// </summary>
        [Map("limit_limit_fok")]
        LimitFillOrKill,
        /// <summary>
        /// ["<c>stop_limit_stop_limit_gtc</c>"] Stop order, active until canceled
        /// </summary>
        [Map("stop_limit_stop_limit_gtc")]
        StopLimit,
        /// <summary>
        /// ["<c>stop_limit_stop_limit_gtd</c>"] Stop order, active until a certain date
        /// </summary>
        [Map("stop_limit_stop_limit_gtd")]
        StopLimitGoodTillDate,
        /// <summary>
        /// ["<c>trigger_bracket_gtc</c>"] Limit order with stop limit order parameters embedded in the order, active until canceled
        /// </summary>
        [Map("trigger_bracket_gtc")]
        Bracket,
        /// <summary>
        /// ["<c>trigger_bracket_gtd</c>"] Limit order with stop limit order parameters embedded in the order, active until a certain date
        /// </summary>
        [Map("trigger_bracket_gtd")]
        BracketGoodTillDate
    }
}
