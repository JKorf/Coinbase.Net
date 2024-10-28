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

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CoinbaseOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            AdvancedTrade = new OrderBookFactory<CoinbaseOrderBookOptions>(
                Create,
                (sharedSymbol, options) => Create(CoinbaseExchange.FormatSymbol(sharedSymbol.BaseAsset, sharedSymbol.QuoteAsset, sharedSymbol.TradingMode, sharedSymbol.DeliverTime), options));
        }

         /// <inheritdoc />
        public IOrderBookFactory<CoinbaseOrderBookOptions> AdvancedTrade { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<CoinbaseOrderBookOptions>? options = null)
        {
            var symbolName = CoinbaseExchange.FormatSymbol(symbol.BaseAsset, symbol.QuoteAsset, symbol.TradingMode, symbol.DeliverTime);
            return Create(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook Create(string symbol, Action<CoinbaseOrderBookOptions>? options = null)
            => new CoinbaseSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICoinbaseSocketClient>());


    }
}
