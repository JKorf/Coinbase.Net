using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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
