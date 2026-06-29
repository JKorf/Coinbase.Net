using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc />
    public class CoinbaseUserClientProvider : UserClientProvider<
        ICoinbaseRestClient,
        ICoinbaseSocketClient,
        CoinbaseRestOptions,
        CoinbaseSocketOptions,
        CoinbaseCredentials,
        CoinbaseEnvironment
        >, ICoinbaseUserClientProvider
    {
                /// <inheritdoc />
        public override string ExchangeName => CoinbaseExchange.ExchangeName;

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
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override ICoinbaseRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<CoinbaseRestOptions> options) 
            => new CoinbaseRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override ICoinbaseSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<CoinbaseSocketOptions> options) 
            => new CoinbaseSocketClient(options, loggerFactory);
    }
}
