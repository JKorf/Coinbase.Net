using Coinbase.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Coinbase.Net.Interfaces.Clients.ExchangeApi;

/// <summary>
/// Coinbase exchange api exchange data endpoints. Exchange data includes market data and currency data.
/// </summary>
public interface ICoinbaseRestClientExchangeApiExchangeData
{
    /// <summary>
    /// Get the current server time
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);


    /// <summary>
    /// Get the list of supported currencies
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<CoinbaseExAsset[]>> GetCoinbaseExAssetAsync(CancellationToken ct = default);


    /// <summary>
    /// Get the list of supported trading pairs
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<WebCallResult<CoinbaseExSymbol[]>> GetCoinbaseExSymbolAsync(CancellationToken ct = default);
}