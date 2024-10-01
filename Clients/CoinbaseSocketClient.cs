using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using System;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.Interfaces.Clients.SpotApi;
using Coinbase.Net.Clients.SpotApi;

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

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of CoinbaseSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        public CoinbaseSocketClient(ILoggerFactory? loggerFactory = null) : this((x) => { }, loggerFactory)
        {
        }

        /// <summary>
        /// Create a new instance of CoinbaseSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CoinbaseSocketClient(Action<CoinbaseSocketOptions> optionsDelegate) : this(optionsDelegate, null)
        {
        }

        /// <summary>
        /// Create a new instance of CoinbaseSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CoinbaseSocketClient(Action<CoinbaseSocketOptions>? optionsDelegate, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Coinbase")
        {
            var options = CoinbaseSocketOptions.Default.Copy();
            optionsDelegate?.Invoke(options);
            Initialize(options);

            AdvancedTradeApi = AddApiClient(new CoinbaseSocketClientAdvancedTradeApi(_logger, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<CoinbaseSocketOptions> optionsDelegate)
        {
            var options = CoinbaseSocketOptions.Default.Copy();
            optionsDelegate(options);
            CoinbaseSocketOptions.Default = options;
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            AdvancedTradeApi.SetApiCredentials(credentials);
        }
    }
}
