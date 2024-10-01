﻿using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Symbol type
    /// </summary>
    public enum SymbolType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_PRODUCT_TYPE")]
        Unknown,
        /// <summary>
        /// Spot symbol
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Futures symbol
        /// </summary>
        [Map("FUTURE")]
        Futures
    }
}