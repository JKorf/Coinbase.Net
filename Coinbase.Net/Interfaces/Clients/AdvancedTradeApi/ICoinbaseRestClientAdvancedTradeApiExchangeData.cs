using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get list of supported symbols
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproducts" /></para>
        /// </summary>
        /// <param name="type">Type of symbol</param>
        /// <param name="expiryType">Type of expiry</param>
        /// <param name="expireStatus">Status of futures expiry status</param>
        /// <param name="allProducts">Return all symbols</param>
        /// <param name="symbols">Filter by symbol names</param>
        /// <param name="limit">Max number of resulst</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseSymbol>>> GetSymbolsAsync(SymbolType? type = null, ContractExpiryType? expiryType = null, ExpiryStatus? expireStatus = null, bool? allProducts = null, IEnumerable<string>? symbols = null, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific symbol
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproduct" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the order book
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproductbook" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="limit">Book depth</param>
        /// <param name="priceIntervals">Grouping of prices</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, decimal? priceIntervals = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpubliccandles" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="klineInterval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseKline>>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get historical public trades for a symbol
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicmarkettrades" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseTrades>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the best ask/bid price and quantity for a symbol
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getbestbidask" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the best ask/bid price and quantity for all or selected symbols
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getbestbidask" /></para>
        /// </summary>
        /// <param name="symbols">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<CoinbaseBookTicker>>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get fiat assets
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-currencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseFiatAsset>>> GetFiatAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get crypto assets
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-currencies" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbaseCryptoAsset>>> GetCryptoAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange rates
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-exchange-rates" /></para>
        /// </summary>
        /// <param name="asset">The asset to get exchange rates for, defaults to USD</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseExchangeRates>> GetExchangeRatesAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current buy prices for all assets denoted in the asset parameter. Includes a 1% Coinbase fee.
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbasePrice>>> GetBuyPriceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the current sell prices for all assets denoted in the asset parameter. Includes a 1% Coinbase fee.
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbasePrice>>> GetSellPriceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the spot market prices for all assets denoted in the asset parameter
        /// <para><a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="date">Specify for retrieving a historical price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CoinbasePrice>>> GetSpotPriceAsync(string asset, DateTime? date = null, CancellationToken ct = default);
    }
}
