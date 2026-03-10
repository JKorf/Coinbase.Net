using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// ["<c>UNKNOWN_TIME_IN_FORCE</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_TIME_IN_FORCE")]
        Unknown,
        /// <summary>
        /// ["<c>GOOD_UNTIL_DATE_TIME</c>"] Good until date
        /// </summary>
        [Map("GOOD_UNTIL_DATE_TIME")]
        GoodTillDate,
        /// <summary>
        /// ["<c>GOOD_UNTIL_CANCELLED</c>"] Good until canceled
        /// </summary>
        [Map("GOOD_UNTIL_CANCELLED")]
        GoodTillCanceled,
        /// <summary>
        /// ["<c>IMMEDIATE_OR_CANCEL</c>"] Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>FILL_OR_KILL</c>"] Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL")]
        FillOrKill
    }
}
