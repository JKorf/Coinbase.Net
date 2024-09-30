using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Objects;

namespace Coinbase.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Coinbase Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface ICoinbaseRestClientSpotApiExchangeData
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

    }
}
