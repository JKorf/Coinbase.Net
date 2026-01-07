using Coinbase.Net.Clients.MessageHandlers;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.ExchangeApi;

/// <summary>
/// Client providing access to the Coinbase Exchange rest Api
/// </summary>
internal class CoinbaseRestClientExchangeApi : RestApiClient, ICoinbaseRestClientExchangeApi
{
    #region fields 
    protected override IRestMessageHandler MessageHandler { get; } = new CoinbaseRestMessageHandler(CoinbaseErrors.Errors);
    protected override ErrorMapping ErrorMapping => CoinbaseErrors.Errors;
    #endregion

    #region Api clients
    /// <inheritdoc />
    public ICoinbaseRestClientExchangeApiExchangeData ExchangeData { get; }
    /// <inheritdoc />
    public string ExchangeName => "Coinbase";
    #endregion

    #region constructor/destructor
    internal CoinbaseRestClientExchangeApi(CoinbaseRestClient baseClient, ILogger logger, HttpClient? httpClient, CoinbaseRestOptions options)
        : base(logger, httpClient, options.Environment.ExchangeRestClientAddress, options, options.ExchangeOptions)
    {
        ExchangeData = new CoinbaseRestClientExchangeApiExchangeData(this);

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
}