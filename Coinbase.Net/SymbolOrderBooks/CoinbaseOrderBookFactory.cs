using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Coinbase.Net.Interfaces;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Coinbase.Net.SymbolOrderBooks
{
    /// <summary>
    /// Coinbase order book factory
    /// </summary>
    public class CoinbaseOrderBookFactory : ICoinbaseOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <inheritdoc />
        public string ExchangeName => CoinbaseExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CoinbaseOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            AdvancedTrade = new OrderBookFactory<CoinbaseOrderBookOptions>(Create, Create);
        }

         /// <inheritdoc />
        public IOrderBookFactory<CoinbaseOrderBookOptions> AdvancedTrade { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<CoinbaseOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(CoinbaseExchange.FormatSymbol);
            return Create(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<CoinbaseOrderBookOptions>? options = null)
            => new CoinbaseSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseSocketClient>());


    }
}
