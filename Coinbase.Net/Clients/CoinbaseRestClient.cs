using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Clients.AdvancedTradeApi;
using Coinbase.Net.Clients.ExchangeApi;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc cref="ICoinbaseRestClient" />
    public class CoinbaseRestClient : BaseRestClient, ICoinbaseRestClient
    {
        #region Api clients

         /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApi AdvancedTradeApi { get; }
         /// <inheritdoc />
        public ICoinbaseRestClientExchangeApi ExchangeApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the CoinbaseRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CoinbaseRestClient(Action<CoinbaseRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the CoinbaseRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public CoinbaseRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<CoinbaseRestOptions> options) : base(loggerFactory, "Coinbase")
        {
            Initialize(options.Value);

            AdvancedTradeApi = AddApiClient(new CoinbaseRestClientAdvancedTradeApi(this, _logger, httpClient, options.Value));
            ExchangeApi = AddApiClient(new CoinbaseRestClientExchangeApi(this, _logger, httpClient, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            AdvancedTradeApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CoinbaseRestOptions> optionsDelegate)
        {
            CoinbaseRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            AdvancedTradeApi.SetApiCredentials(credentials);
        }
    }
}
