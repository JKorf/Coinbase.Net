﻿using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Portfolio type
    /// </summary>
    public enum PortfolioType
    {
        /// <summary>
        /// Undefined
        /// </summary>
        [Map("UNDEFINED")]
        Undefined,
        /// <summary>
        /// Default
        /// </summary>
        [Map("DEFAULT")]
        Default,
        /// <summary>
        /// Consumer
        /// </summary>
        [Map("CONSUMER")]
        Consumer,
        /// <summary>
        /// International
        /// </summary>
        [Map("INTX")]
        International
    }
}
