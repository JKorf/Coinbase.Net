using Coinbase.Net.Clients;
using Coinbase.Net.Interfaces;
using Coinbase.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Coinbase.Net
{
    /// <inheritdoc />
    public class CoinbaseTrackerFactory : ICoinbaseTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CoinbaseTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<ICoinbaseSocketClient>() ?? new CoinbaseSocketClient()).AdvancedTradeApi.SharedClient;
            return client.SubscribeKlineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<ICoinbaseRestClient>() ?? new CoinbaseRestClient()).AdvancedTradeApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<ICoinbaseSocketClient>()?? new CoinbaseSocketClient()).AdvancedTradeApi.SharedClient;

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                restClient,
                socketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = (_serviceProvider?.GetRequiredService<ICoinbaseRestClient>() ?? new CoinbaseRestClient()).AdvancedTradeApi.SharedClient;
            var socketClient = (_serviceProvider?.GetRequiredService<ICoinbaseSocketClient>() ?? new CoinbaseSocketClient()).AdvancedTradeApi.SharedClient;

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                null,
                restClient,
                socketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<ICoinbaseRestClient>() ?? new CoinbaseRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<ICoinbaseSocketClient>() ?? new CoinbaseSocketClient();
            return new CoinbaseUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CoinbaseUserSpotDataTracker>>() ?? new NullLogger<CoinbaseUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, CoinbaseEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<ICoinbaseUserClientProvider>() ?? new CoinbaseUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new CoinbaseUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CoinbaseUserSpotDataTracker>>() ?? new NullLogger<CoinbaseUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(FuturesUserDataTrackerConfig? config = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<ICoinbaseRestClient>() ?? new CoinbaseRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<ICoinbaseSocketClient>() ?? new CoinbaseSocketClient();
            return new CoinbaseUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CoinbaseUserFuturesDataTracker>>() ?? new NullLogger<CoinbaseUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, CoinbaseEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<ICoinbaseUserClientProvider>() ?? new CoinbaseUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new CoinbaseUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<CoinbaseUserFuturesDataTracker>>() ?? new NullLogger<CoinbaseUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
