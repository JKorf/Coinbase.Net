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
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="pageCursor">["<c>cursor</c>"] Cursor from last request to retrieve the next page</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
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
        /// <param name="type">["<c>portfolio_type</c>"] Filter by portfolio type</param>
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
        /// <param name="portfolioId">["<c>portfolioId</c>"] Id of the portfolio</param>
        /// <param name="asset">["<c>currency</c>"] Filter by asset</param>
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
        /// <param name="portfolioName">["<c>name</c>"] Name of the new portfolio</param>
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
        /// <param name="fromPortfolioId">["<c>source_portfolio_uuid</c>"] From portfolio</param>
        /// <param name="toPortfolioId">["<c>target_portfolio_uuid</c>"] To portfolio</param>
        /// <param name="quantity">["<c>value</c>"] Quantity</param>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
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
        /// <param name="portfolioId">["<c>portfolioId</c>"] Id of portfolio</param>
        /// <param name="newName">["<c>name</c>"] New name</param>
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
        /// <param name="portfolioId">["<c>portfolio_uuid</c>"] Id of the portfolio</param>
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
        /// <param name="portfolioId">["<c>portfolio_uuid</c>"] Portfolio id</param>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to be allocated for the specified isolated position.</param>
        /// <param name="asset">["<c>currency</c>"] The asset to be allocated for the specific isolated position</param>
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
        /// <param name="portfolioId">["<c>portfolioId</c>"] Portfolio uuid</param>
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
        /// <param name="portfolioId">["<c>portfolioId</c>"] Portfolio uuid</param>
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
        /// <param name="portfolioId">["<c>portfolio_uuid</c>"] Portfolio uuid</param>
        /// <param name="enabled">["<c>multi_asset_collateral_enabled</c>"] Enabled</param>
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
        /// <param name="setting">["<c>setting</c>"] Setting value</param>
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
        /// <param name="marginProfileType">["<c>margin_profile_type</c>"] Margin profile type</param>
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
        /// <param name="symbolType">["<c>product_type</c>"] Type of product</param>
        /// <param name="expiryType">["<c>contract_expiry_type</c>"] Expiry type</param>
        /// <param name="venue">["<c>product_venue</c>"] Venue</param>
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
        /// <param name="paymentMethodId">["<c>paymentMethodId</c>"] Payment method id</param>
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
        /// <param name="fromAsset">["<c>from_account</c>"] The asset to convert from</param>
        /// <param name="toAsset">["<c>to_account</c>"] The asset to convert to</param>
        /// <param name="quantity">["<c>amount</c>"] The quantity to convert in fromAsset</param>
        /// <param name="userIncentiveId">["<c>user_incentive_id</c>"] The user incentive id</param>
        /// <param name="promoCode">["<c>code_val</c>"] The promo code</param>
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
        /// <param name="tradeId">["<c>tradeId</c>"] Id of the trade</param>
        /// <param name="fromAsset">["<c>from_account</c>"] From asset</param>
        /// <param name="toAsset">["<c>to_account</c>"] To asset</param>
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
        /// <param name="tradeId">["<c>tradeId</c>"] Id of the quote</param>
        /// <param name="fromAsset">["<c>from_account</c>"] From asset</param>
        /// <param name="toAsset">["<c>to_account</c>"] To asset</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="order">["<c>order</c>"] Result order</param>
        /// <param name="fromId">["<c>starting_after</c>"] Return results before after id</param>
        /// <param name="toId">["<c>ending_before</c>"] Return results before this id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="withdrawalId">["<c>withdrawalId</c>"] Withdrawal id</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to withdraw</param>
        /// <param name="paymentMethod">["<c>payment_method</c>"] Payment method id</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="paymentId">["<c>payment_method</c>"] Payment id</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity to deposit</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="order">["<c>order</c>"] Result order</param>
        /// <param name="fromId">["<c>starting_after</c>"] Return results before after id</param>
        /// <param name="toId">["<c>ending_before</c>"] Return results before this id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="depositId">["<c>depositId</c>"] Deposit id</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="order">["<c>order</c>"] Result order</param>
        /// <param name="fromId">["<c>starting_after</c>"] Return results before after id</param>
        /// <param name="toId">["<c>ending_before</c>"] Return results before this id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="transactionId">["<c>transactionId</c>"] Transaction id</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="addressId">["<c>addressId</c>"] Address id</param>
        /// <param name="order">["<c>order</c>"] Result order</param>
        /// <param name="fromId">["<c>starting_after</c>"] Return results before after id</param>
        /// <param name="toId">["<c>ending_before</c>"] Return results before this id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="to">["<c>to</c>"] Blockchain addres or email address of recipient</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to send</param>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `ETH`</param>
        /// <param name="description">["<c>description</c>"] Description</param>
        /// <param name="network">["<c>network</c>"] Network to use for the withdrawal</param>
        /// <param name="idempotencyToken">["<c>idem</c>"] If a previous transaction with the same idempotencyToken parameter exists for this sender, that previous transaction is returned and a new one is not created. Max length is 100 characters.</param>
        /// <param name="destinationTag">["<c>destination_tag</c>"] Destination tag</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="name">["<c>name</c>"] Address label</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="order">["<c>order</c>"] Result order</param>
        /// <param name="fromId">["<c>starting_after</c>"] Return results before after id</param>
        /// <param name="toId">["<c>ending_before</c>"] Return results before this id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
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
        /// <param name="accountId">["<c>accountId</c>"] Account id</param>
        /// <param name="addressId">["<c>addressId</c>"] Id of the address</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CoinbaseDepositAddress>> GetDepositAddressAsync(string accountId, string addressId, CancellationToken ct = default);

    }
}
