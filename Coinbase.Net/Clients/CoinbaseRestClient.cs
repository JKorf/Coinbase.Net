using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Coinbase.Net.Clients.SpotApi;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc cref="ICoinbaseRestClient" />
    public class CoinbaseRestClient : BaseRestClient, ICoinbaseRestClient
    {
        #region Api clients

         /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApi AdvancedTradeApi { get; }


        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the CoinbaseRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CoinbaseRestClient(Action<CoinbaseRestOptions>? optionsDelegate = null) : this(null, null, optionsDelegate)
        {
        }

        /// <summary>
        /// Create a new instance of the CoinbaseRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public CoinbaseRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, Action<CoinbaseRestOptions>? optionsDelegate = null) : base(loggerFactory, "Coinbase")
        {
            var options = CoinbaseRestOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            AdvancedTradeApi = AddApiClient(new CoinbaseRestClientAdvancedTradeApi(this, _logger, httpClient, options));
        }

        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CoinbaseRestOptions> optionsDelegate)
        {
            var options = CoinbaseRestOptions.Default.Copy();
            optionsDelegate(options);
            CoinbaseRestOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            AdvancedTradeApi.SetApiCredentials(credentials);
        }
    }
}
