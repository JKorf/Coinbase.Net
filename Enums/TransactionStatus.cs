using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Transaction status
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// Transaction was canceled
        /// </summary>
        [Map("canceled")]
        Canceled,
        /// <summary>
        /// Completed transactions (e.g., a send or a buy)
        /// </summary>
        [Map("completed")]
        Completed,
        /// <summary>
        /// Conditional transaction expired due to external factors
        /// </summary>
        [Map("expired")]
        Expired,
        /// <summary>
        /// Failed transactions (e.g., failed buy)
        /// </summary>
        [Map("failed")]
        Failed,
        /// <summary>
        /// Pending transactions (e.g., a send or a buy)
        /// </summary>
        [Map("pending")]
        Pending,
        /// <summary>
        /// Vault withdrawal is waiting to be cleared
        /// </summary>
        [Map("waiting_for_clearing")]
        WaitingForClearing,
        /// <summary>
        /// Vault withdrawal is waiting for approval
        /// </summary>
        [Map("waiting_for_signature")]
        WaitingForSignature
    }
}
