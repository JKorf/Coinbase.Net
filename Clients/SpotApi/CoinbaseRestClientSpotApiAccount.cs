using CryptoExchange.Net.Objects;
using Coinbase.Net.Clients.SpotApi;
using Coinbase.Net.Interfaces.Clients.SpotApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;
using System.Globalization;

namespace Coinbase.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientSpotApiAccount : ICoinbaseRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientSpotApi _baseClient;

        internal CoinbaseRestClientSpotApiAccount(CoinbaseRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Accounts

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseAccountPage>> GetAccountsAsync(int? limit = null, string? pageCursor = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("limit", limit);
            parameters.AddOptional("cursor", pageCursor);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/accounts", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseAccountPage>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseAccount>> GetAccountAsync(string accountId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/accounts/{accountId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseAccountWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data.Account);
        }

        #endregion

        #region Get Portfolios

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbasePortfolio>>> GetPortfoliosAsync(PortfolioType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("portfolio_type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/portfolios", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfoliosWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data.Portfolios);
        }

        #endregion

        #region Get Portfolio

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePorfolioBreakdown>> GetPortfolioAsync(string portfolioId, string? asset = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/portfolios/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePorfolioBreakdownWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePorfolioBreakdown>(result.Data?.Breakdown);
        }

        #endregion

        #region Create Portfolio

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePortfolio>> CreatePortfolioAsync(string portfolioName, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("name", portfolioName);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/brokerage/portfolios", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfolioWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data.Portfolio);
        }

        #endregion

        #region Transfer Portfolio funds

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePortfolioMove>> TransferPortfolioFundsAsync(string fromPortfolioId, string toPortfolioId, decimal quantity, string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("source_portfolio_uuid", fromPortfolioId);
            parameters.Add("target_portfolio_uuid", toPortfolioId);
            parameters.Add("funds", new
            {
                value = quantity.ToString(CultureInfo.InvariantCulture),
                currency = asset
            });
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/brokerage/portfolios/move_funds", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfolioMove>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Portfolio

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePortfolio>> EditPortfolioAsync(string portfolioId, string newName, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("name", newName);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/brokerage/portfolios/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfolioWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data.Portfolio);
        }

        #endregion

        #region Delete Portfolio

        /// <inheritdoc />
        public async Task<WebCallResult> DeletePortfolioAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("portfolio_uuid", portfolioId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"api/v3/brokerage/portfolios/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
