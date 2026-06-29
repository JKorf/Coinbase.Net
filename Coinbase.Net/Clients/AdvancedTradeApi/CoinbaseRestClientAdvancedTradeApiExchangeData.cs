using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using System.Linq;
using CryptoExchange.Net.Objects.Errors;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientAdvancedTradeApiExchangeData : ICoinbaseRestClientAdvancedTradeApiExchangeData
    {
        private readonly CoinbaseRestClientAdvancedTradeApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal CoinbaseRestClientAdvancedTradeApiExchangeData(ILogger logger, CoinbaseRestClientAdvancedTradeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/brokerage/time", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseTime>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Time);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseSymbol[]>> GetSymbolsAsync(SymbolType? type = null, ContractExpiryType? expiryType = null, ExpiryStatus? expireStatus = null, bool? allProducts = null, IEnumerable<string>? symbols = null, bool getTradabilityStatus = false, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("product_type", type);
            parameters.AddArray("product_ids", symbols?.ToArray());
            parameters.Add("contract_expiry_type", expiryType);
            parameters.Add("expiring_contract_status", expireStatus);
            parameters.Add("get_all_products", allProducts);
            RequestDefinition request;
            if (!_baseClient.Authenticated)
            {
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/brokerage/market/products", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            }
            else
            {
                parameters.Add("get_tradability_status", getTradabilityStatus);
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/api/v3/brokerage/products", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            }
            var result = await _baseClient.SendAsync<CoinbaseSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Symbols);
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            RequestDefinition request;
            if (!_baseClient.Authenticated)
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"api/v3/brokerage/market/products/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            else
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/products/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseSymbol>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, decimal? priceIntervals = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("product_id", symbol);
            parameters.Add("limit", limit);
            parameters.Add("aggregation_price_increment", priceIntervals);

            RequestDefinition request;
            if (!_baseClient.Authenticated)
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/market/product_book", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            else
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/product_book", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseOrderBookWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseOrderBook>(result);

            return HttpResult.Ok(result, result.Data.Book);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseKline[]>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("granularity", klineInterval);
            parameters.Add("start", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("end", endTime, DateTimeSerialization.SecondsString);
            parameters.Add("limit", limit);

            RequestDefinition request;
            if (!_baseClient.Authenticated)
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/market/products/{symbol}/candles", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            else
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/products/{symbol}/candles", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseKlineWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseKline[]>(result);

            return HttpResult.Ok(result, result.Data.Klines);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseTrades>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("start", startTime, DateTimeSerialization.SecondsString);
            parameters.Add("end", endTime, DateTimeSerialization.SecondsString);
            parameters.Add("limit", limit);


            RequestDefinition request;
            if (!_baseClient.Authenticated)
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/market/products/{symbol}/ticker", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            else
                request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/products/{symbol}/ticker", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseTrades>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("product_ids", new[] { symbol });
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/best_bid_ask", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseBookTickerWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseBookTicker>(result);

            if (!result.Data.Data.Any())
                return HttpResult.Fail<CoinbaseBookTicker>(result, new ServerError(new ErrorInfo(ErrorType.Unknown, "Not found")));

            return HttpResult.Ok(result, result.Data.Data.Single());
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseBookTicker[]>> GetBookTickersAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.AddArray("product_ids", symbols?.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/api/v3/brokerage/best_bid_ask", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);

            var result = await _baseClient.SendAsync<CoinbaseBookTickerWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseBookTicker[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Fiat Assets

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseFiatAsset[]>> GetFiatAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/currencies", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseFiatAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseFiatAsset[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Crypto Assets

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseCryptoAsset[]>> GetCryptoAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "v2/currencies/crypto", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseCryptoAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseCryptoAsset[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Exchange Rates

        /// <inheritdoc />
        public async Task<HttpResult<CoinbaseExchangeRates>> GetExchangeRatesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v2/exchange-rates", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseExchangeRatesWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbaseExchangeRates>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Buy Price

        /// <inheritdoc />
        public async Task<HttpResult<CoinbasePrice[]>> GetBuyPriceAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v2/prices/{asset}/buy", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbasePriceWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbasePrice[]>(result);

            return HttpResult.Ok(result, result.Data.Prices);
        }

        #endregion

        #region Get Buy Price

        /// <inheritdoc />
        public async Task<HttpResult<CoinbasePrice[]>> GetSellPriceAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v2/prices/{asset}/sell", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbasePriceWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbasePrice[]>(result);

            return HttpResult.Ok(result, result.Data.Prices);
        }

        #endregion

        #region Get Spot Price

        /// <inheritdoc />
        public async Task<HttpResult<CoinbasePrice[]>> GetSpotPriceAsync(string asset, DateTime? date, CancellationToken ct = default)
        {
            var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
            parameters.Add("date", date?.ToString("yyyy-MM-dd"));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v2/prices/{asset}/sell", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbasePriceWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CoinbasePrice[]>(result);

            return HttpResult.Ok(result, result.Data.Prices);
        }

        #endregion
    }
}
