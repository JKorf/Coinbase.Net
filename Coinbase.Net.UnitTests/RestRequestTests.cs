using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Authentication;
using System.Linq;

namespace Coinbase.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateAdvancedTradeAccountCalls()
        {
            var client = new CoinbaseRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "-----BEGIN EC PRIVATE KEY-----\r\nMHcCAQEEIGaopmcUKDBihelMJbKUyRmaR6F3Eo90EZaqZJ3/mBr0oAoGCCqGSM49\r\nAwEHoUQDQgAEnYaxPG+o57xM5o/M5QNn0ocwlw12ZNVWFEo9tKDQ7Jz5Gz/0eMcP\r\nmEhm5msFFpWgrY0/T92MfwByuaLws/rM3w==\r\n-----END EC PRIVATE KEY-----");
            });
            var tester = new RestRequestValidator<CoinbaseRestClient>(client, "Endpoints/AdvancedTrade/Account", "https://api.coinbase.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetAccountsAsync(), "GetAccounts");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetFeeInfoAsync(), "GetFeeInfo");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetApiKeyInfoAsync(), "GetApiKeyInfo");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetPaymentMethodsAsync(), "GetPaymentMethods", nestedJsonProperty: "payment_methods");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.WithdrawCryptoAsync("123", "123", 0.1m, "123"), "WithdrawCrypto", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.CreateDepositAddressAsync("123", "123"), "CreateDepositAddress", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetDepositAddressAsync("123", "123"), "GetDepositAddress", "data", ignoreProperties: new List<string> { "callback_url" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetAddressTransactionsAsync("123", "123"), "GetAddressTransactions");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetPortfoliosAsync(), "GetPortfolios", "portfolios");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetPortfolioAsync("123"), "GetPortfolio", "breakdown");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.CreatePortfolioAsync("123"), "CreatePortfolio", "portfolio");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.TransferPortfolioFundsAsync("123", "123", 1, "ETH"), "TransferPortfolioFunds");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.EditPortfolioAsync("123", "123"), "EditPortfolio", "portfolio");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.DeletePortfolioAsync("123"), "DeletePortfolio");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.CreateConvertQuoteAsync("ETH", "ETH", 1), "CreateConvertQuote", "trade");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetConvertTradeAsync("123", "ETH", "ETH"), "GetConvertTrade", "trade");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.CommitConvertTradeAsync("123", "ETH", "ETH"), "CommitConvertTrade", "trade");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetWithdrawalsAsync("123"), "GetWithdrawals");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetWithdrawalAsync("123", "123"), "GetWithdrawal", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.WithdrawAsync("123", "ETH", 123, "etheruem"), "Withdraw", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.DepositAsync("123", "123", "ETH", 1), "Deposit", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetDepositsAsync("123"), "GetDeposits");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetDepositAsync("123", "123"), "GetDeposit", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetTransactionsAsync("123"), "GetTransactions");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetTransactionAsync("123", "123"), "GetTransaction", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetDepositAddressesAsync("123"), "GetDepositAddresses");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetPerpetualBalancesAsync("123"), "GetPerpetualBalances", "portfolio_balances");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.SetPerpetualMultiAssetCollateralModeAsync("123", true), "SetPerpetualMultiAssetCollateralMode");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetFuturesBalanceSummaryAsync(), "GetFuturesBalanceSummary", nestedJsonProperty: "balance_summary");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.SetFuturesIntradayMarginSettingAsync(IntradayMargin.Standard), "SetIntradayMarginSetting");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetFuturesIntradayMarginSettingAsync(), "GetIntradayMarginSetting");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Account.GetFuturesCurrentMarginWindowAsync(MarginProfileType.Regular), "GetFuturesCurrentMarginWindow");
        }

        [Test]
        public async Task ValidateAdvancedTradeExchangeDataCalls()
        {
            var client = new CoinbaseRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<CoinbaseRestClient>(client, "Endpoints/AdvancedTrade/ExchangeData", "https://api.coinbase.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", "products");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetKlinesAsync("ETH-USDT", KlineInterval.OneDay), "GetKlines", "candles");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetTradeHistoryAsync("ETH-USDT"), "GetTradeHistory", ignoreProperties: new List<string> { "bid", "ask" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetFiatAssetsAsync(), "GetFiatAssets", nestedJsonProperty: "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetCryptoAssetsAsync(), "GetCryptoAssets", "data");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.ExchangeData.GetExchangeRatesAsync(), "GetExchangeRates", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateAdvancedTradeTradingCalls()
        {
            var client = new CoinbaseRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new ApiCredentials("123", "-----BEGIN EC PRIVATE KEY-----\r\nMHcCAQEEIGaopmcUKDBihelMJbKUyRmaR6F3Eo90EZaqZJ3/mBr0oAoGCCqGSM49\r\nAwEHoUQDQgAEnYaxPG+o57xM5o/M5QNn0ocwlw12ZNVWFEo9tKDQ7Jz5Gz/0eMcP\r\nmEhm5msFFpWgrY0/T92MfwByuaLws/rM3w==\r\n-----END EC PRIVATE KEY-----");
            });
            var tester = new RestRequestValidator<CoinbaseRestClient>(client, "Endpoints/AdvancedTrade/Trading", "https://api.coinbase.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.PlaceOrderAsync("ETHUSDT", OrderSide.Sell, NewOrderType.Limit), "PlaceOrder", ignoreProperties: new List<string> { "order_configuration" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.CancelOrdersAsync(new[] { "123" }), "CancelOrders", nestedJsonProperty: "results");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.EditOrderAsync("123", 1, 1), "EditOrder", ignoreProperties: new List<string> { "errors" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.GetOrderAsync("123"), "GetOrder", nestedJsonProperty: "order");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.GetOrdersAsync(), "GetOrders", nestedJsonProperty: "orders", ignoreProperties: new List<string> { "order_configuration" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.GetUserTradesAsync(), "GetUserTrades");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.ClosePositionAsync("ETH-USDT", 1), "ClosePosition", ignoreProperties: new List<string> { "order_configuration" });
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.GetFuturesPositionsAsync(), "GetFuturesPositions", nestedJsonProperty: "positions");
            await tester.ValidateAsync(client => client.AdvancedTradeApi.Trading.GetPerpetualPositionAsync("123", "BTC-PERP-INTX"), "GetPerpetualPosition", "position");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(x => x.Key == "Authorization");
        }
    }
}
