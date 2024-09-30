using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;

namespace Coinbase.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Coinbase Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface ICoinbaseRestClientSpotApiAccount
    {
        /// <summary>
        /// Get accounts
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getaccounts" /></para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="pageCursor">Cursor from last request to retrieve the next page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseAccountPage>> GetAccountsAsync(int? limit = null, string? pageCursor = null, CancellationToken ct = default);

        /// <summary>
        /// Get a specific account
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getaccount" /></para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseAccount>> GetAccountAsync(string accountId, CancellationToken ct = default);

        /// <summary>
        /// Get a list of portfolios
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getportfolios" /></para>
        /// </summary>
        /// <param name="type">Filter by portfolio type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<CoinbasePortfolio>>> GetPortfoliosAsync(PortfolioType? type = null, CancellationToken ct = default);

        /// <summary>
        /// Get a breakdown of a portfolio
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getportfoliobreakdown" /></para>
        /// </summary>
        /// <param name="portfolioId">Id of the portfolio</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePorfolioBreakdown>> GetPortfolioAsync(string portfolioId, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new portfolio
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_createportfolio" /></para>
        /// </summary>
        /// <param name="portfolioName">Name of the new portfolio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolio>> CreatePortfolioAsync(string portfolioName, CancellationToken ct = default);

        /// <summary>
        /// Move funds between portfolios
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_moveportfoliofunds" /></para>
        /// </summary>
        /// <param name="fromPortfolioId">From portfolio</param>
        /// <param name="toPortfolioId">To portfolio</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolioMove>> TransferPortfolioFundsAsync(string fromPortfolioId, string toPortfolioId, decimal quantity, string asset, CancellationToken ct = default);

        /// <summary>
        /// Delete a portfolio
        /// <para><a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_deleteportfolio" /></para>
        /// </summary>
        /// <param name="portfolioId">Id of the portfolio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> DeletePortfolioAsync(string portfolioId, CancellationToken ct = default);
    }
}
