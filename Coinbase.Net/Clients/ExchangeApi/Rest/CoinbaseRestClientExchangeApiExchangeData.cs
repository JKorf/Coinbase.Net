using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Clients.ExchangeApi;

internal class CoinbaseRestClientExchangeApiExchangeData : ICoinbaseRestClientExchangeApiExchangeData
{
    private readonly CoinbaseRestClientExchangeApi _baseClient;
    private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

    internal CoinbaseRestClientExchangeApiExchangeData(CoinbaseRestClientExchangeApi baseClient)
    {
        _baseClient = baseClient;
    }

    #region Get Server Time

    /// <inheritdoc />
    public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "time", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
        var result = await _baseClient.SendAsync<CoinbaseTime>(request, null, ct).ConfigureAwait(false);
        if (!result.Success)
            return HttpResult.Fail<DateTime>(result);

        return HttpResult.Ok<DateTime>(result, result.Data.Time);
    }

    #endregion

    #region Get Assets

    /// <inheritdoc />
    public async Task<HttpResult<CoinbaseExAsset[]>> GetAssetsAsync(CancellationToken ct = default)
    {
        var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "currencies", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
        var result = await _baseClient.SendAsync<CoinbaseExAsset[]> (request, parameters, ct).ConfigureAwait(false);
        return result;
    }

    #endregion

    #region Get Symbols
    /// <inheritdoc />
    public async Task<HttpResult<CoinbaseExSymbol[]>> GetSymbolsAsync(CancellationToken ct = default)
    {
        var parameters = new Parameters(CoinbaseExchange._parameterSerializationSettings);
        var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "products", CoinbaseExchange.RateLimiter.CoinbaseRestPublic, 1, false);
        var result = await _baseClient.SendAsync<CoinbaseExSymbol[]>(request, parameters, ct).ConfigureAwait(false);
        return result;
    }
    #endregion
}