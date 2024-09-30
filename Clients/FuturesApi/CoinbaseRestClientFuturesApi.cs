using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using Coinbase.Net.Interfaces.Clients.FuturesApi;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using Coinbase.Net.Objects;

namespace Coinbase.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="ICoinbaseRestClientFuturesApi" />
    internal partial class CoinbaseRestClientFuturesApi : RestApiClient, ICoinbaseRestClientFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Futures Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public ICoinbaseRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public ICoinbaseRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public ICoinbaseRestClientFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Coinbase";
        #endregion

        #region constructor/destructor
        internal CoinbaseRestClientFuturesApi(ILogger logger, HttpClient? httpClient, CoinbaseRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.FuturesOptions)
        {
            Account = new CoinbaseRestClientFuturesApiAccount(this);
            ExchangeData = new CoinbaseRestClientFuturesApiExchangeData(logger, this);
            Trading = new CoinbaseRestClientFuturesApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);

            // Optional response checking

            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) => throw new NotImplementedException();

        /// <inheritdoc />
        public ICoinbaseRestClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) => FormatSymbol(baseAsset, quoteAsset, TradingMode.Spot);

    }
}
