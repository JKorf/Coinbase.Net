using CryptoExchange.Net.Objects.Options;
using System;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Options for the Coinbase SymbolOrderBook
    /// </summary>
    public class CoinbaseOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        public static CoinbaseOrderBookOptions Default { get; set; } = new CoinbaseOrderBookOptions();

        /// <summary>
        /// After how much time we should consider the connection dropped if no data is received for this time after the initial subscriptions
        /// </summary>
        public TimeSpan? InitialDataTimeout { get; set; }

        internal CoinbaseOrderBookOptions Copy()
        {
            var result = Copy<CoinbaseOrderBookOptions>();
            result.InitialDataTimeout = InitialDataTimeout;
            return result;
        }
    }
}
