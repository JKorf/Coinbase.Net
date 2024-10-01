using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Coinbase.Net.Interfaces;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;

namespace Coinbase.Net.SymbolOrderBooks
{
    /// <summary>
    /// Coinbase order book factory
    /// </summary>
    public class CoinbaseOrderBookFactory : ICoinbaseOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CoinbaseOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            AdvancedTrade = new OrderBookFactory<CoinbaseOrderBookOptions>((symbol, options) => Create(symbol, options), (baseAsset, quoteAsset, options) => Create(baseAsset + "-" + quoteAsset, options));
        }

         /// <inheritdoc />
        public IOrderBookFactory<CoinbaseOrderBookOptions> AdvancedTrade { get; }

         /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<CoinbaseOrderBookOptions>? options = null)
            => new CoinbaseSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseRestClient>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseSocketClient>());


    }
}
