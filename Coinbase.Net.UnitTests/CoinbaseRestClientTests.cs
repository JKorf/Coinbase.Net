using CryptoExchange.Net.Clients;
using NUnit.Framework;
using System.Collections.Generic;
using Coinbase.Net.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CryptoExchange.Net.Objects;
using Coinbase.Net.Interfaces.Clients;

namespace Coinbase.Net.UnitTests
{
    [TestFixture()]
    public class CoinbaseRestClientTests
    {
        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<CoinbaseRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<CoinbaseSocketClient>();
        }

        [Test]
        [TestCase(TradeEnvironmentNames.Live, "https://api.coinbase.com")]
        [TestCase("", "https://api.coinbase.com")]
        public void TestConstructorEnvironments(string environmentName, string expected)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Coinbase:Environment:Name", environmentName },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCoinbase(configuration.GetSection("Coinbase"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICoinbaseRestClient>();

            var address = client.AdvancedTradeApi.BaseAddress;

            Assert.That(address, Is.EqualTo(expected));
        }

        [Test]
        public void TestConstructorNullEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Coinbase", null },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCoinbase(configuration.GetSection("Coinbase"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICoinbaseRestClient>();

            var address = client.AdvancedTradeApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.coinbase.com"));
        }

        [Test]
        public void TestConstructorApiOverwriteEnvironment()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "Coinbase:Environment:Name", "test" },
                    { "Coinbase:Rest:Environment:Name", "live" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCoinbase(configuration.GetSection("Coinbase"));
            var provider = collection.BuildServiceProvider();

            var client = provider.GetRequiredService<ICoinbaseRestClient>();

            var address = client.AdvancedTradeApi.BaseAddress;

            Assert.That(address, Is.EqualTo("https://api.coinbase.com"));
        }

        [Test]
        public void TestConstructorConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "ApiCredentials:Key", "123" },
                    { "ApiCredentials:Secret", "456" },
                    { "ApiCredentials:Memo", "000" },
                    { "Socket:ApiCredentials:Key", "456" },
                    { "Socket:ApiCredentials:Secret", "789" },
                    { "Socket:ApiCredentials:Memo", "xxx" },
                    { "Rest:OutputOriginalData", "true" },
                    { "Socket:OutputOriginalData", "false" },
                    { "Rest:Proxy:Host", "host" },
                    { "Rest:Proxy:Port", "80" },
                    { "Socket:Proxy:Host", "host2" },
                    { "Socket:Proxy:Port", "81" },
                }).Build();

            var collection = new ServiceCollection();
            collection.AddCoinbase(configuration);
            var provider = collection.BuildServiceProvider();

            var restClient = provider.GetRequiredService<ICoinbaseRestClient>();
            var socketClient = provider.GetRequiredService<ICoinbaseSocketClient>();

            Assert.That(((BaseApiClient)restClient.AdvancedTradeApi).OutputOriginalData, Is.True);
            Assert.That(((BaseApiClient)socketClient.AdvancedTradeApi).OutputOriginalData, Is.False);
            Assert.That(((BaseApiClient)restClient.AdvancedTradeApi).AuthenticationProvider.ApiKey, Is.EqualTo("123"));
            Assert.That(((BaseApiClient)socketClient.AdvancedTradeApi).AuthenticationProvider.ApiKey, Is.EqualTo("456"));
            Assert.That(((BaseApiClient)restClient.AdvancedTradeApi).ClientOptions.Proxy.Host, Is.EqualTo("host"));
            Assert.That(((BaseApiClient)restClient.AdvancedTradeApi).ClientOptions.Proxy.Port, Is.EqualTo(80));
            Assert.That(((BaseApiClient)socketClient.AdvancedTradeApi).ClientOptions.Proxy.Host, Is.EqualTo("host2"));
            Assert.That(((BaseApiClient)socketClient.AdvancedTradeApi).ClientOptions.Proxy.Port, Is.EqualTo(81));
        }
    }
}
