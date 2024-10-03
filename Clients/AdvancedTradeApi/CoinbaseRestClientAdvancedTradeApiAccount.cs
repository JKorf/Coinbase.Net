using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Enums;
using System.Globalization;
using Coinbase.Net.Clients.SpotApi;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <inheritdoc />
    internal class CoinbaseRestClientAdvancedTradeApiAccount : ICoinbaseRestClientAdvancedTradeApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CoinbaseRestClientAdvancedTradeApi _baseClient;

        internal CoinbaseRestClientAdvancedTradeApiAccount(CoinbaseRestClientAdvancedTradeApi baseClient)
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
            return result.As<CoinbaseAccount>(result.Data?.Account);
        }

        #endregion

        #region Get Portfolios

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbasePortfolio>>> GetPortfoliosAsync(PortfolioType? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("portfolio_type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/portfolios", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfoliosWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbasePortfolio>>(result.Data?.Portfolios);
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
            return result.As<CoinbasePortfolio>(result.Data?.Portfolio);
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
            var request = _definitions.GetOrCreate(HttpMethod.Put, $"api/v3/brokerage/portfolios/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePortfolioWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePortfolio>(result.Data?.Portfolio);
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

        #region Delete Portfolio

        /// <inheritdoc />
        public async Task<WebCallResult> AllocatePortfolioAsync(string portfolioId, string symbol, decimal quantity, string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("portfolio_uuid", portfolioId);
            parameters.Add("symbol", portfolioId);
            parameters.AddString("amount", quantity);
            parameters.Add("currency", portfolioId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/brokerage/intx/allocate", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Perpetual Portfolio Summary

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualPorfolios>> GetPerpetualPortfolioSummaryAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/portfolio/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPorfolios>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Perpetual Positions

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualPositions>> GetPerpetualPositionsAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/positions/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositions>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Perpetual Position

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualPosition>> GetPerpetualPositionAsync(string portfolioId, string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/positions/{portfolioId}/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePerpetualPosition>(result.Data?.Position);
        }

        #endregion

        #region Get Perpetual Balances

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePerpetualBalances>> GetPerpetualBalancesAsync(string portfolioId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/api/v3/brokerage/intx/balances/{portfolioId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePerpetualBalancesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePerpetualBalances>(result.Data?.PortfolioBalances);
        }

        #endregion

        #region Get Perpetual Balances

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseMultiAssetMode>> SetPerpetualMultiAssetCollateralModeAsync(string portfolioId, bool enabled, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("portfolio_uuid", portfolioId);
            parameters.Add("multi_asset_collateral_enabled", enabled);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/api/v3/brokerage/intx/multi_asset_collateral", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseMultiAssetMode>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Balance Summary

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseFuturesBalanceSummary>> GetFuturesBalanceSummaryAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/brokerage/cfm/balance_summary", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesBalanceSummaryWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<CoinbaseFuturesBalanceSummary>(default);

            if (result.Data.BalanceSummary == null)
                return result.AsError<CoinbaseFuturesBalanceSummary>(new ServerError("Not found"));

            return result.As(result.Data.BalanceSummary);
        }

        #endregion

        #region Get Fee Info

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseFeeInfo>> GetFeeInfoAsync(SymbolType? symbolType = null, ContractExpiryType? expiryType = null, string? venue = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("product_type", symbolType);
            parameters.AddOptionalEnum("contract_expiry_type", expiryType);
            parameters.AddOptional("product_venue", venue);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/transaction_summary", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFeeInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Futures Intraday Margin Setting

        /// <inheritdoc />
        public async Task<WebCallResult> SetFuturesIntradayMarginSettingAsync(IntradayMargin setting, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("setting", setting);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/brokerage/cfm/intraday/margin_setting", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Intraday Margin Setting

        /// <inheritdoc />
        public async Task<WebCallResult<IntradayMarginSetting>> GetFuturesIntradayMarginSettingAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/cfm/intraday/margin_setting", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<IntradayMarginSetting>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Current Margin Window

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseFuturesMarginWindow>> GetFuturesCurrentMarginWindowAsync(MarginProfileType marginProfileType, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("margin_profile_type", marginProfileType);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/brokerage/cfm/intraday/current_margin_window", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesMarginWindow>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Futures Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbaseFuturesPosition>>> GetFuturesPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/cfm/positions", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionsWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbaseFuturesPosition>>(result.Data?.Positions);
        }

        #endregion

        #region Get Futures Position

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseFuturesPosition>> GetFuturesPositionAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/cfm/positions/{symbol}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseFuturesPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseFuturesPosition>(result.Data?.Position);
        }

        #endregion

        #region Get Api Key Info

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseApiKey>> GetApiKeyInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/key_permissions", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseApiKey>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Payment Methods

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CoinbasePaymentMethod>>> GetPaymentMethodsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "api/v3/brokerage/payment_methods", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaymentMethodsWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CoinbasePaymentMethod>>(result.Data?.PaymentMethods);
        }

        #endregion

        #region Get Payment Method

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaymentMethod>> GetPaymentMethodAsync(string paymentMethodId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/payment_methods/{paymentMethodId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaymentMethodWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbasePaymentMethod>(result.Data?.PaymentMethod);
        }

        #endregion

        #region Create Convert Quote

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseConvertQuote>> CreateConvertQuoteAsync(string fromAsset, string toAsset, decimal quantity, string? userIncentiveId = null, string? promoCode = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("from_account", fromAsset);
            parameters.Add("to_account", toAsset);
            parameters.AddString("amount", quantity);
            parameters.AddOptional("user_incentive_id", userIncentiveId);
            parameters.AddOptional("code_val", promoCode);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/brokerage/convert/quote", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseConvertQuoteWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseConvertQuote>(result.Data?.Trade);
        }

        #endregion

        #region Get Convert Quote

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseConvertQuote>> GetConvertTradeAsync(string tradeId, string fromAsset, string toAsset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("from_account", fromAsset);
            parameters.Add("to_account", toAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"api/v3/brokerage/convert/trade/{tradeId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseConvertQuoteWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseConvertQuote>(result.Data?.Trade);
        }

        #endregion

        #region Commit Convert Quote

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseConvertQuote>> CommitConvertTradeAsync(string tradeId, string fromAsset, string toAsset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("from_account", fromAsset);
            parameters.Add("to_account", toAsset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"api/v3/brokerage/convert/trade/{tradeId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseConvertQuoteWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseConvertQuote>(result.Data?.Trade);
        }

        #endregion

        #region Get Withdrawals

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaginatedResult<CoinbaseWithdrawal>>> GetWithdrawalsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("order", order);
            parameters.AddOptional("starting_after", fromId);
            parameters.AddOptional("ending_before", toId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/withdrawals", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaginatedResult<CoinbaseWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseWithdrawal>> GetWithdrawalAsync(string accountId, string withdrawalId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/withdrawals/{withdrawalId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseWithdrawalWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseWithdrawal>(result.Data?.Data);
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseWithdrawal>> WithdrawAsync(string accountId, string asset, decimal quantity, string paymentMethod, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.AddString("amount", quantity);
            parameters.Add("payment_method", paymentMethod);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v2/accounts/{accountId}/withdrawals", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseWithdrawalWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseWithdrawal>(result.Data?.Data);
        }

        #endregion

        #region Deposit

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseDeposit>> DepositAsync(string accountId, string paymentId, string asset, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("payment_method", paymentId);
            parameters.Add("currency", asset);
            parameters.AddString("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v2/accounts/{accountId}/deposits", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseDepositWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseDeposit>(result.Data?.Data);
        }

        #endregion

        #region Get Deposits

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaginatedResult<CoinbaseDeposit>>> GetDepositsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("order", order);
            parameters.AddOptional("starting_after", fromId);
            parameters.AddOptional("ending_before", toId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/deposits", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaginatedResult<CoinbaseDeposit>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseDeposit>> GetDepositAsync(string accountId, string depositId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/deposits/{depositId}", CoinbaseExchange.RateLimiter.CoinbaseRestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseDepositWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseDeposit>(result.Data?.Data);
        }

        #endregion

        #region Get Transactions

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaginatedResult<CoinbaseTransaction>>> GetTransactionsAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("order", order);
            parameters.AddOptional("starting_after", fromId);
            parameters.AddOptional("ending_before", toId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/transactions", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaginatedResult<CoinbaseTransaction>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transaction

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseTransaction>> GetTransactionAsync(string accountId, string transactionId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/transactions/{transactionId}", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseTransactionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseTransaction>(result.Data?.Data);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseTransaction>> TransferAsync(string accountId, string toAccountId, decimal quantity, string asset, string? description = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("type", "transfer");
            parameters.Add("accountId", accountId);
            parameters.Add("to", toAccountId);
            parameters.AddString("amount", quantity);
            parameters.Add("currency", asset);
            parameters.AddOptional("description", description);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v2/accounts/{accountId}/transactions", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseTransactionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseTransaction>(result.Data?.Data);
        }

        #endregion

        #region Get Address Transactions

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaginatedResult<CoinbaseTransaction>>> GetAddressTransactionsAsync(string accountId, string addressId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("order", order);
            parameters.AddOptional("starting_after", fromId);
            parameters.AddOptional("ending_before", toId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/addresses/{addressId}/transactions", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaginatedResult<CoinbaseTransaction>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw Crypto

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseTransaction>> WithdrawCryptoAsync(string accountId, string to, decimal quantity, string asset, string? description = null, bool? skipNotifications = null, string? idempotencyToken = null, bool? toFinancialInstitution = null, string? financialInstituionWebsite = null, string? destinationTag = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("accountId", accountId);
            parameters.Add("to", to);
            parameters.AddString("amount", quantity);
            parameters.Add("currency", asset);
            parameters.AddOptional("description", description);
            parameters.AddOptional("skip_notifications", skipNotifications);
            parameters.AddOptional("idem", idempotencyToken);
            parameters.AddOptional("to_financial_institution", toFinancialInstitution);
            parameters.AddOptional("financial_institution_website", financialInstituionWebsite);
            parameters.AddOptional("destination_tag", destinationTag);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v2/accounts/{accountId}/transactions", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseTransactionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseTransaction>(result.Data?.Data);
        }

        #endregion

        #region Create Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseDepositAddress>> CreateDepositAddressAsync(string accountId, string name, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("name", name);
            var request = _definitions.GetOrCreate(HttpMethod.Post, $"/v2/accounts/{accountId}/addresses", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseDepositAddressWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseDepositAddress>(result.Data?.Data);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbasePaginatedResult<CoinbaseDepositAddress>>> GetDepositAddressesAsync(string accountId, SortOrder? order = null, string? fromId = null, string? toId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("order", order);
            parameters.AddOptional("starting_after", fromId);
            parameters.AddOptional("ending_before", toId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/addresses", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbasePaginatedResult<CoinbaseDepositAddress>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<WebCallResult<CoinbaseDepositAddress>> GetDepositAddressAsync(string accountId, string addressId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v2/accounts/{accountId}/addresses/{addressId}", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, true);
            var result = await _baseClient.SendAsync<CoinbaseDepositAddressWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CoinbaseDepositAddress>(result.Data?.Data);
        }

        #endregion

    }
}
