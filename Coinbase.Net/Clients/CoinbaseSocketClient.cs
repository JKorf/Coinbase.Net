using Coinbase.Net.Clients.AdvancedTradeApi;
using Coinbase.Net.Clients.ExchangeApi;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc cref="ICoinbaseSocketClient" />
    public class CoinbaseSocketClient : BaseSocketClient, ICoinbaseSocketClient
    {
        #region fields
        #endregion

        #region Api clients

         /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeApi AdvancedTradeApi { get; }

        /// <inheritdoc />
        public ICoinbaseSocketClientExchangeApi ExchangeApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of CoinbaseSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CoinbaseSocketClient(Action<CoinbaseSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of CoinbaseSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public CoinbaseSocketClient(IOptions<CoinbaseSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Coinbase")
        {
            Initialize(options.Value);

            AdvancedTradeApi = AddApiClient(new CoinbaseSocketClientAdvancedTradeApi(_logger, options.Value));
            ExchangeApi = AddApiClient(new CoinbaseSocketClientExchangeApi(_logger, options.Value));
        }
        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            AdvancedTradeApi.SetOptions(options);
            ExchangeApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CoinbaseSocketOptions> optionsDelegate)
        {
            CoinbaseSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            AdvancedTradeApi.SetApiCredentials(credentials);
            ExchangeApi.SetApiCredentials(credentials);
        }
    }
}
