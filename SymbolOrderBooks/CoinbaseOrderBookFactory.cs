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
            
            
            Futures = new OrderBookFactory<CoinbaseOrderBookOptions>((symbol, options) => CreateFutures(symbol, options), (baseAsset, quoteAsset, options) => CreateFutures(baseAsset + quoteAsset, options));

            Spot = new OrderBookFactory<CoinbaseOrderBookOptions>((symbol, options) => CreateSpot(symbol, options), (baseAsset, quoteAsset, options) => CreateSpot(baseAsset + quoteAsset, options));

        }

        
         /// <inheritdoc />
        public IOrderBookFactory<CoinbaseOrderBookOptions> Futures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<CoinbaseOrderBookOptions> Spot { get; }


        
         /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<CoinbaseOrderBookOptions>? options = null)
            => new CoinbaseFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseRestClient>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<CoinbaseOrderBookOptions>? options = null)
            => new CoinbaseSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseRestClient>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseSocketClient>());


    }
}
