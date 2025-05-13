using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Created
        /// </summary>
        [Map("created")]
        Created,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("completed")]
        Completed,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("canceled")]
        Canceled
    }
}
