using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    public enum PositionSide
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Long position
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_LONG")]
        Long,
        /// <summary>
        /// Short position
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_SHORT")]
        Short
    }
}
