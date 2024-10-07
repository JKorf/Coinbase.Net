using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    public enum TimeInForce
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_TIME_IN_FORCE")]
        Unknown,
        /// <summary>
        /// Good until date
        /// </summary>
        [Map("GOOD_UNTIL_DATE_TIME")]
        GoodTillDate,
        /// <summary>
        /// Good until canceled
        /// </summary>
        [Map("GOOD_UNTIL_CANCELLED")]
        GoodTillCanceled,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("FILL_OR_KILL")]
        FillOrKill
    }
}
