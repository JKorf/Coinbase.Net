using Coinbase.Net.Clients;
using Coinbase.Net.Objects.Models;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Coinbase.Net.UnitTests
{
    [NonParallelizable]
    internal class CoinbaseSocketIntegrationTests : SocketIntegrationTest<CoinbaseSocketClient>
    {
        public override bool Run { get; set; } = false;

        public CoinbaseSocketIntegrationTests()
        {
        }

        public override CoinbaseSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new CoinbaseSocketClient(Options.Create(new CoinbaseSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            await RunAndCheckUpdate<CoinbaseFuturesBalance>(useUpdatedDeserialization , (client, updateHandler) => client.AdvancedTradeApi.SubscribeToFuturesBalanceUpdatesAsync(updateHandler, default), false, true);
            await RunAndCheckUpdate<CoinbaseTicker>(useUpdatedDeserialization , (client, updateHandler) => client.AdvancedTradeApi.SubscribeToTickerUpdatesAsync("ETH-USD", updateHandler, default), true, false);
        } 
    }
}
