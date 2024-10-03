using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Converters.MessageParsing;
using System.Reflection;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

namespace Coinbase.Net.Clients.SpotApi
{
    /// <inheritdoc cref="ICoinbaseRestClientAdvancedTradeApi" />
    internal partial class CoinbaseRestClientAdvancedTradeApi : RestApiClient, ICoinbaseRestClientAdvancedTradeApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");
        #endregion

        #region Api clients
        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApiAccount Account { get; }
        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Coinbase";
        #endregion

        #region constructor/destructor
        internal CoinbaseRestClientAdvancedTradeApi(ILogger logger, HttpClient? httpClient, CoinbaseRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.SpotOptions)
        {
            Account = new CoinbaseRestClientAdvancedTradeApiAccount(this);
            ExchangeData = new CoinbaseRestClientAdvancedTradeApiExchangeData(logger, this);
            Trading = new CoinbaseRestClientAdvancedTradeApiTrading(logger, this);

            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            var version = Assembly.GetAssembly(typeof(RestApiClient)).GetName().Version;
            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + version.ToString() }
            };
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

        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, IMessageAccessor accessor)
        {
            if (!accessor.IsJson)
                return new ServerError(accessor.GetOriginalString());

            var error = accessor.GetValue<string>(MessagePath.Get().Property("error"));
            if (error == null)
            {
                var errorId = accessor.GetValue<string?>(MessagePath.Get().Property("errors").Index(0).Property("id"));
                var errorMsg = accessor.GetValue<string?>(MessagePath.Get().Property("errors").Index(0).Property("message"));
                if (errorId != null)
                    return new ServerError($"{errorId}: {errorMsg}");

                return new ServerError(accessor.GetOriginalString());
            }

            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            return new ServerError($"{error}: {msg}");
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
        public ICoinbaseRestClientAdvancedTradeApiShared SharedClient => this;

        /// <inheritdoc />
        public string GetSymbolName(string baseAsset, string quoteAsset) => FormatSymbol(baseAsset, quoteAsset, TradingMode.Spot);

    }
}
