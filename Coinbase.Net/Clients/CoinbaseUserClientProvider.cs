using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc />
    public class CoinbaseUserClientProvider : ICoinbaseUserClientProvider
    {
        private static ConcurrentDictionary<string, ICoinbaseRestClient> _restClients = new ConcurrentDictionary<string, ICoinbaseRestClient>();
        private static ConcurrentDictionary<string, ICoinbaseSocketClient> _socketClients = new ConcurrentDictionary<string, ICoinbaseSocketClient>();

        private readonly IOptions<CoinbaseRestOptions> _restOptions;
        private readonly IOptions<CoinbaseSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => CoinbaseExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public CoinbaseUserClientProvider(Action<CoinbaseOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<CoinbaseRestOptions> restOptions,
            IOptions<CoinbaseSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, CoinbaseEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public ICoinbaseRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, CoinbaseEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public ICoinbaseSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, CoinbaseEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private ICoinbaseRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, CoinbaseEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new CoinbaseRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private ICoinbaseSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, CoinbaseEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new CoinbaseSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<CoinbaseRestOptions> SetRestEnvironment(CoinbaseEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new CoinbaseRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<CoinbaseSocketOptions> SetSocketEnvironment(CoinbaseEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new CoinbaseSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
