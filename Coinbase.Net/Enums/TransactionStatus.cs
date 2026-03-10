using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionStatus>))]
    public enum TransactionStatus
    {
        /// <summary>
        /// ["<c>canceled</c>"] Transaction was canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// ["<c>completed</c>"] Completed transactions (e.g., a send or a buy)
        /// </summary>
        [Map("completed")]
        Completed,
        /// <summary>
        /// ["<c>expired</c>"] Conditional transaction expired due to external factors
        /// </summary>
        [Map("expired")]
        Expired,
        /// <summary>
        /// ["<c>failed</c>"] Failed transactions (e.g., failed buy)
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// ["<c>pending</c>"] Pending transactions (e.g., a send or a buy)
        /// </summary>
        [Map("pending")]
        Pending,
        /// <summary>
        /// ["<c>waiting_for_clearing</c>"] Vault withdrawal is waiting to be cleared
        /// </summary>
        [Map("waiting_for_clearing")]
        WaitingForClearing,
        /// <summary>
        /// ["<c>waiting_for_signature</c>"] Vault withdrawal is waiting for approval
        /// </summary>
        [Map("waiting_for_signature")]
        WaitingForSignature
    }
}
