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
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproducts" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/market/products<br />
        /// GET /api/v3/brokerage/products
        /// </para>
        /// </summary>
        /// <param name="type">["<c>product_type</c>"] Type of symbol</param>
        /// <param name="expiryType">["<c>contract_expiry_type</c>"] Type of expiry</param>
        /// <param name="expireStatus">["<c>expiring_contract_status</c>"] Status of futures expiry status</param>
        /// <param name="allProducts">["<c>get_all_products</c>"] Return all symbols</param>
        /// <param name="symbols">["<c>product_ids</c>"] Filter by symbol names</param>
        /// <param name="getTradabilityStatus">["<c>get_tradability_status</c>"] Whether or not to populate <see cref="CoinbaseSymbol.ViewOnly"/> with the tradability status of the product. This is only enabled for SPOT products. Can only be used when the client is authenticated.</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="offset">Result offset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseSymbol[]>> GetSymbolsAsync(SymbolType? type = null, ContractExpiryType? expiryType = null, ExpiryStatus? expireStatus = null, bool? allProducts = null, IEnumerable<string>? symbols = null, bool getTradabilityStatus = false, int? limit = null, int? offset = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproduct" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/market/products/{symbol}<br />
        /// GET /api/v3/brokerage/products/{symbol}
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicproductbook" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/market/product_book<br />
        /// GET /api/v3/brokerage/product_book
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>product_id</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="limit">["<c>limit</c>"] Book depth</param>
        /// <param name="priceIntervals">["<c>aggregation_price_increment</c>"] Grouping of prices</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, decimal? priceIntervals = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpubliccandles" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/market/products/{symbol}/candles<br />
        /// GET /api/v3/brokerage/products/{symbol}/candles
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="klineInterval">["<c>granularity</c>"] Kline interval</param>
        /// <param name="startTime">["<c>start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseKline[]>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get historical public trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpublicmarkettrades" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/market/products/{symbol}/ticker<br />
        /// GET /api/v3/brokerage/products/{symbol}/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>start</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseTrades>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get the best ask/bid price and quantity for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getbestbidask" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/best_bid_ask
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>product_ids</c>"] The symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the best ask/bid price and quantity for all or selected symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getbestbidask" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/best_bid_ask
        /// </para>
        /// </summary>
        /// <param name="symbols">["<c>product_ids</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseBookTicker[]>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get fiat assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-currencies" /><br />
        /// Endpoint:<br />
        /// GET /v2/currencies
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFiatAsset[]>> GetFiatAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get crypto assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-currencies" /><br />
        /// Endpoint:<br />
        /// GET /v2/currencies/crypto
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseCryptoAsset[]>> GetCryptoAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-exchange-rates" /><br />
        /// Endpoint:<br />
        /// GET /v2/exchange-rates
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset to get exchange rates for, defaults to USD</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseExchangeRates>> GetExchangeRatesAsync(string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current buy prices for all assets denoted in the asset parameter. Includes a 1% Coinbase fee.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /><br />
        /// Endpoint:<br />
        /// GET /v2/prices/{asset}/buy
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePrice[]>> GetBuyPriceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the current sell prices for all assets denoted in the asset parameter. Includes a 1% Coinbase fee.
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /><br />
        /// Endpoint:<br />
        /// GET /v2/prices/{asset}/sell
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePrice[]>> GetSellPriceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get the spot market prices for all assets denoted in the asset parameter
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-prices" /><br />
        /// Endpoint:<br />
        /// GET /v2/prices/{asset}/sell
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Asset name</param>
        /// <param name="date">["<c>date</c>"] Specify for retrieving a historical price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePrice[]>> GetSpotPriceAsync(string asset, DateTime? date = null, CancellationToken ct = default);
    }
}
