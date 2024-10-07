using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;
using Coinbase.Net.Clients.SpotApi;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/time", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.Time ?? default);
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseSymbol>>> GetSymbolsAsync(SymbolType? type = null, ContractExpiryType? expiryType = null, ExpiryStatus? expireStatus = null, bool? allProducts = null, IEnumerable<string>? symbols = null, int? limit = null, int? offset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("product_type", type);
            parameters.AddOptional("product_ids", symbols);
            parameters.AddOptionalEnum("contract_expiry_type", expiryType);
            parameters.AddOptionalEnum("expiring_contract_status", expireStatus);
            parameters.AddOptional("get_all_products", allProducts);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/brokerage/market/products", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseSymbol>>(result.Data?.Symbols);
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/market/products/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseSymbol>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, decimal? priceIntervals = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("product_id", symbol);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalString("aggregation_price_increment", priceIntervals);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/market/product_book", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseOrderBookWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseOrderBook>(result.Data?.Book);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseKline>>> GetKlinesAsync(string symbol, KlineInterval klineInterval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("granularity", klineInterval);
            parameters.AddOptionalMillisecondsString("start", startTime);
            parameters.AddOptionalMillisecondsString("end", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/market/products/{symbol}/candles", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseKlineWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseKline>>(result.Data?.Klines);
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseTrades>> GetTradeHistoryAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalSecondsString("start", startTime);
            parameters.AddOptionalSecondsString("end", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/market/products/{symbol}/ticker", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseTrades>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fiat Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseFiatAsset>>> GetFiatAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/currencies", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseFiatAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseFiatAsset>>(result.Data?.Data);
        }

        #endregion

        #region Get Crypto Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseCryptoAsset>>> GetCryptoAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "v2/currencies/crypto", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseCryptoAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseCryptoAsset>>(result.Data?.Data);
        }

        #endregion

        #region Get Exchange Rates

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseExchangeRates>> GetExchangeRatesAsync(string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v2/exchange-rates", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
            var result = await _baseClient.SendAsync<CoinbaseExchangeRatesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseExchangeRates>(result.Data?.Data);
        }

        #endregion

    }
}
