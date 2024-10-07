using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status
    /// </summary>
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
