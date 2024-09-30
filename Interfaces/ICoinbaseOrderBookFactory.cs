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
        /// Futures order book factory methods
        /// </summary>
        IOrderBookFactory<CoinbaseOrderBookOptions> Futures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<CoinbaseOrderBookOptions> Spot { get; }


        
        /// <summary>
        /// Create a new Futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<CoinbaseOrderBookOptions>? options);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<CoinbaseOrderBookOptions>? options);

    }
}