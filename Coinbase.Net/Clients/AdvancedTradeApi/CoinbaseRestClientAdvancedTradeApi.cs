using Coinbase.Net.Clients.MessageHandlers;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.AdvancedTradeApi
{
    /// <inheritdoc cref="ICoinbaseRestClientAdvancedTradeApi" />
    internal partial class CoinbaseRestClientAdvancedTradeApi : RestApiClient, ICoinbaseRestClientAdvancedTradeApi
    {
        #region fields 
        protected override IRestMessageHandler MessageHandler { get; } = new CoinbaseRestMessageHandler(CoinbaseErrors.Errors);
        protected override ErrorMapping ErrorMapping => CoinbaseErrors.Errors;
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
        internal CoinbaseRestClientAdvancedTradeApi(CoinbaseRestClient baseClient, ILogger logger, HttpClient? httpClient, CoinbaseRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress, options, options.AdvancedTradeOptions)
        {
            Account = new CoinbaseRestClientAdvancedTradeApiAccount(this);
            ExchangeData = new CoinbaseRestClientAdvancedTradeApiExchangeData(logger, this);
            Trading = new CoinbaseRestClientAdvancedTradeApiTrading(logger, this);

            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + baseClient.CryptoExchangeLibVersion }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CoinbaseAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            return await base.SendAsync(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CoinbaseExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeApiShared SharedClient => this;

    }
}
