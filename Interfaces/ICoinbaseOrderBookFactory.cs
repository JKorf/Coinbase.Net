using CryptoExchange.Net.Interfaces;
using System;
using Coinbase.Net.Objects.Options;

namespace Coinbase.Net.Interfaces
{
    /// <summary>
    /// Coinbase local order book factory
    /// </summary>
    public interface ICoinbaseOrderBookFactory
    {
        /// <summary>
        /// Order book factory methods
        /// </summary>
        IOrderBookFactory<CoinbaseOrderBookOptions> AdvancedTrade { get; }

        /// <summary>
        /// Create a new local order book instance
        /// </summary>
        ISymbolOrderBook Create(string symbol, Action<CoinbaseOrderBookOptions>? options);
    }
}