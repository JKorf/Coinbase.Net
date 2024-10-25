using CryptoExchange.Net.Interfaces;
using System;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

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
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<CoinbaseOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new local order book instance
        /// </summary>
        ISymbolOrderBook Create(string symbol, Action<CoinbaseOrderBookOptions>? options);
    }
}