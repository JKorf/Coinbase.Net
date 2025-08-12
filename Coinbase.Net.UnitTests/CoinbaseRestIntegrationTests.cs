using Coinbase.Net.Clients;
using Coinbase.Net.SymbolOrderBooks;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.UnitTests
{
    [NonParallelizable]
    internal class CoinbaseRestIntegrationTests : RestIntegrationTest<CoinbaseRestClient>
    {
        public override bool Run { get; set; } = true;

        public CoinbaseRestIntegrationTests()
        {
            CoinbaseExchange.RateLimiter.RateLimitTriggered += (x) => Debug.WriteLine(x);
        }

        public override CoinbaseRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CoinbaseRestClient(null, loggerFactory, Options.Create(new Objects.Options.CoinbaseRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().AdvancedTradeApi.ExchangeData.GetSymbolAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.ErrorCode, Is.EqualTo("INVALID_ARGUMENT"));
            Assert.That(result.Error.ErrorType, Is.EqualTo(ErrorType.InvalidParameter));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.AdvancedTradeApi.Account.GetAccountsAsync(default, default, default), true);
            await RunAndCheckResult(client => client.AdvancedTradeApi.Account.GetPortfoliosAsync(default, default), true);
            await RunAndCheckResult(client => client.AdvancedTradeApi.Account.GetFeeInfoAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.AdvancedTradeApi.Account.GetApiKeyInfoAsync(default), true);
            await RunAndCheckResult(client => client.AdvancedTradeApi.Account.GetPaymentMethodsAsync(default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetSymbolsAsync(default, default, default, default, default, default, default, default, default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetSymbolAsync("ETH-USD", default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetOrderBookAsync("ETH-USD", 1, default, default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetKlinesAsync("ETH-USD", Enums.KlineInterval.OneDay, default, default, 5, default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetTradeHistoryAsync("ETH-USD", default, default, default, default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetFiatAssetsAsync(default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetCryptoAssetsAsync(default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetExchangeRatesAsync(default, default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetBuyPriceAsync("USD", default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetSellPriceAsync("USD", default), false);
            await RunAndCheckResult(client => client.AdvancedTradeApi.ExchangeData.GetSpotPriceAsync("USD", default, default), false);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.AdvancedTradeApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.AdvancedTradeApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new CoinbaseSymbolOrderBook("ETH-USD"));
        }
    }
}
