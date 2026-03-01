using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;

namespace Coinbase.Net.Interfaces.Clients.AdvancedTradeApi
{
    /// <summary>
    /// Coinbase account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface ICoinbaseRestClientAdvancedTradeApiAccount
    {
        /// <summary>
        /// Get accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getaccounts" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/accounts
        /// </para>
        /// </summary>
        /// <param name="limit">Max number of results</param>
        /// <param name="pageCursor">Cursor from last request to retrieve the next page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseAccountPage>> GetAccountsAsync(int? limit = null, string? pageCursor = null, CancellationToken ct = default);

        /// <summary>
        /// Get a specific account
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getaccount" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/accounts/{accountId}
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseAccount>> GetAccountAsync(string accountId, CancellationToken ct = default);

        /// <summary>
        /// Get a list of portfolios
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getportfolios" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/portfolios
        /// </para>
        /// </summary>
        /// <param name="type">Filter by portfolio type</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolio[]>> GetPortfoliosAsync(PortfolioType? type = null, CancellationToken ct = default);

        /// <summary>
        /// Get a breakdown of a portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getportfoliobreakdown" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/portfolios/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Id of the portfolio</param>
        /// <param name="asset">Filter by asset</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePorfolioBreakdown>> GetPortfolioAsync(string portfolioId, string? asset = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_createportfolio" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/portfolios
        /// </para>
        /// </summary>
        /// <param name="portfolioName">Name of the new portfolio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolio>> CreatePortfolioAsync(string portfolioName, CancellationToken ct = default);

        /// <summary>
        /// Move funds between portfolios
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_moveportfoliofunds" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/portfolios/move_funds
        /// </para>
        /// </summary>
        /// <param name="fromPortfolioId">From portfolio</param>
        /// <param name="toPortfolioId">To portfolio</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolioMove>> TransferPortfolioFundsAsync(string fromPortfolioId, string toPortfolioId, decimal quantity, string asset, CancellationToken ct = default);

        /// <summary>
        /// Edit portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_editportfolio" /><br />
        /// Endpoint:<br />
        /// PUT /api/v3/brokerage/portfolios/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Id of portfolio</param>
        /// <param name="newName">New name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePortfolio>> EditPortfolioAsync(string portfolioId, string newName, CancellationToken ct = default);

        /// <summary>
        /// Delete a portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_deleteportfolio" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v3/brokerage/portfolios/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Id of the portfolio</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> DeletePortfolioAsync(string portfolioId, CancellationToken ct = default);

        /// <summary>
        /// Allocate portfolio funds to a sub-portfolio on Intx Portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_allocateportfolio" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/intx/allocate
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Portfolio id</param>
        /// <param name="symbol">Symbol</param>
        /// <param name="quantity">Quantity to be allocated for the specified isolated position.</param>
        /// <param name="asset">The asset to be allocated for the specific isolated position</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> AllocatePortfolioAsync(string portfolioId, string symbol, decimal quantity, string asset, CancellationToken ct = default);

        /// <summary>
        /// Get a summary of your Perpetuals portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getintxportfoliosummary" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/intx/portfolio/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Portfolio uuid</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePerpetualPorfolios>> GetPerpetualPortfolioSummaryAsync(string portfolioId, CancellationToken ct = default);

        /// <summary>
        /// Get balances of a Perpetual futures portfolio
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getintxbalances" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/intx/balances/{portfolioId}
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Portfolio uuid</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbasePerpetualBalances>> GetPerpetualBalancesAsync(string portfolioId, CancellationToken ct = default);

        /// <summary>
        /// Set multi asset collateral mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_intxmultiassetcollateral" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/intx/multi_asset_collateral
        /// </para>
        /// </summary>
        /// <param name="portfolioId">Portfolio uuid</param>
        /// <param name="enabled">Enabled</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseMultiAssetMode>> SetPerpetualMultiAssetCollateralModeAsync(string portfolioId, bool enabled, CancellationToken ct = default);

        /// <summary>
        /// Get delivery futures balance summary
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getfcmbalancesummary" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/cfm/balance_summary
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFuturesBalanceSummary>> GetFuturesBalanceSummaryAsync(CancellationToken ct = default);

        /// <summary>
        /// Set intraday margin setting
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_setintradaymarginsetting" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/cfm/intraday/margin_setting
        /// </para>
        /// </summary>
        /// <param name="setting">Setting value</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetFuturesIntradayMarginSettingAsync(IntradayMargin setting, CancellationToken ct = default);

        /// <summary>
        /// Get intraday margin setting
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getintradaymarginsetting" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/cfm/intraday/margin_setting
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IntradayMarginSetting>> GetFuturesIntradayMarginSettingAsync(CancellationToken ct = default);

        /// <summary>
        /// Get futures current margin window
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getcurrentmarginwindow" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/cfm/intraday/current_margin_window
        /// </para>
        /// </summary>
        /// <param name="marginProfileType">Margin profile type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFuturesMarginWindow>> GetFuturesCurrentMarginWindowAsync(MarginProfileType marginProfileType, CancellationToken ct = default);

        /// <summary>
        /// Get fee tier info
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_gettransactionsummary" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/transaction_summary
        /// </para>
        /// </summary>
        /// <param name="symbolType">Type of product</param>
        /// <param name="expiryType">Expiry type</param>
        /// <param name="venue">Venue</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseFeeInfo>> GetFeeInfoAsync(SymbolType? symbolType = null, ContractExpiryType? expiryType = null, string? venue = null, CancellationToken ct = default);

        /// <summary>
        /// Get API key info/permissions for the current API key
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getapikeypermissions" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/key_permissions
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseApiKey>> GetApiKeyInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get payment methods
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpaymentmethods" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/payment_methods
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaymentMethod[]>> GetPaymentMethodsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get payment method by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getpaymentmethods" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/payment_methods/{paymentMethodId}
        /// </para>
        /// </summary>
        /// <param name="paymentMethodId">Payment method id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaymentMethod>> GetPaymentMethodAsync(string paymentMethodId, CancellationToken ct = default);

        /// <summary>
        /// Get a convert quote
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_createconvertquote" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/convert/quote
        /// </para>
        /// </summary>
        /// <param name="fromAsset">The asset to convert from</param>
        /// <param name="toAsset">The asset to convert to</param>
        /// <param name="quantity">The quantity to convert in fromAsset</param>
        /// <param name="userIncentiveId">The user incentive id</param>
        /// <param name="promoCode">The promo code</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseConvertQuote>> CreateConvertQuoteAsync(string fromAsset, string toAsset, decimal quantity, string? userIncentiveId = null, string? promoCode = null, CancellationToken ct = default);

        /// <summary>
        /// Get convert trade info
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_getconverttrade" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/brokerage/convert/trade/{tradeId}
        /// </para>
        /// </summary>
        /// <param name="tradeId">Id of the trade</param>
        /// <param name="fromAsset">From asset</param>
        /// <param name="toAsset">To asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseConvertQuote>> GetConvertTradeAsync(string tradeId, string fromAsset, string toAsset, CancellationToken ct = default);

        /// <summary>
        /// Commit a convert trade
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/advanced-trade/reference/retailbrokerageapi_commitconverttrade" /><br />
        /// Endpoint:<br />
        /// POST /api/v3/brokerage/convert/trade/{tradeId}
        /// </para>
        /// </summary>
        /// <param name="tradeId">Id of the quote</param>
        /// <param name="fromAsset">From asset</param>
        /// <param name="toAsset">To asset</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseConvertQuote>> CommitConvertTradeAsync(string tradeId, string fromAsset, string toAsset, CancellationToken ct = default);

        /// <summary>
        /// Get fiat withdrawls
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-withdrawals" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/withdrawals
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="order">Result order</param>
        /// <param name="fromId">Return results before after id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaginatedResult<CoinbaseWithdrawal>>> GetWithdrawalsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific fiat withdrawal
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-withdrawals" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/withdrawals/{withdrawalId}
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="withdrawalId">Withdrawal id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseWithdrawal>> GetWithdrawalAsync(string accountId, string withdrawalId, CancellationToken ct = default);

        /// <summary>
        /// Withdraw fiat funds
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-withdrawals" /><br />
        /// Endpoint:<br />
        /// POST /v2/accounts/{accountId}/withdrawals
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="paymentMethod">Payment method id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseWithdrawal>> WithdrawAsync(string accountId, string asset, decimal quantity, string paymentMethod, CancellationToken ct = default);

        /// <summary>
        /// Deposit fiat funds
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-deposits" /><br />
        /// Endpoint:<br />
        /// POST /v2/accounts/{accountId}/deposits
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="paymentId">Payment id</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="quantity">Quantity to deposit</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseDeposit>> DepositAsync(string accountId, string paymentId, string asset, decimal quantity, CancellationToken ct = default);

        /// <summary>
        /// Get fiat deposits
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-deposits" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/deposits
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="order">Result order</param>
        /// <param name="fromId">Return results before after id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaginatedResult<CoinbaseDeposit>>> GetDepositsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific fiat deposit
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-deposits" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/deposits/{depositId}
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="depositId">Deposit id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseDeposit>> GetDepositAsync(string accountId, string depositId, CancellationToken ct = default);

        /// <summary>
        /// Get list of transaction for the account
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-transactions" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/transactions
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="order">Result order</param>
        /// <param name="fromId">Return results before after id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaginatedResult<CoinbaseTransaction>>> GetTransactionsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific transaction
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-transactions" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/transactions/{transactionId}
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="transactionId">Transaction id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<CoinbaseTransaction>> GetTransactionAsync(string accountId, string transactionId, CancellationToken ct = default);

        /// <summary>
        /// Get transactions for a specific address
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-addresses" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/addresses/{addressId}/transactions
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="addressId">Address id</param>
        /// <param name="order">Result order</param>
        /// <param name="fromId">Return results before after id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaginatedResult<CoinbaseTransaction>>> GetAddressTransactionsAsync(string accountId, string addressId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw a crypto asset to an external blockchain address or user by email
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-transactions#transfer-money-between-accounts" /><br />
        /// Endpoint:<br />
        /// POST /v2/accounts/{accountId}/transactions
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="to">Blockchain addres or email address of recipient</param>
        /// <param name="quantity">Quantity to send</param>
        /// <param name="asset">The asset, for example `ETH`</param>
        /// <param name="description">Description</param>
        /// <param name="network">Network to use for the withdrawal</param>
        /// <param name="idempotencyToken">If a previous transaction with the same idempotencyToken parameter exists for this sender, that previous transaction is returned and a new one is not created. Max length is 100 characters.</param>
        /// <param name="destinationTag">Destination tag</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseTransaction>> WithdrawCryptoAsync(string accountId, string to, decimal quantity, string asset, string? network = null, string? description = null, string? idempotencyToken = null, string? destinationTag = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new deposit address for an account
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-addresses" /><br />
        /// Endpoint:<br />
        /// POST /v2/accounts/{accountId}/addresses
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="name">Address label</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseDepositAddress>> CreateDepositAddressAsync(string accountId, string name, CancellationToken ct = default);

        /// <summary>
        /// List deposit addresses for an account
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-addresses" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/addresses
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="order">Result order</param>
        /// <param name="fromId">Return results before after id</param>
        /// <param name="toId">Return results before this id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbasePaginatedResult<CoinbaseDepositAddress>>> GetDepositAddressesAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get info on a specific deposit address
        /// <para>
        /// Docs:<br />
        /// <a href="https://docs.cdp.coinbase.com/coinbase-app/docs/api-addresses" /><br />
        /// Endpoint:<br />
        /// GET /v2/accounts/{accountId}/addresses/{addressId}
        /// </para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="addressId">Id of the address</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseDepositAddress>> GetDepositAddressAsync(string accountId, string addressId, CancellationToken ct = default);

    }
}
