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
    /// <para><a href="https://docs.cdp.coinbase.com/api-reference/exchange-api/rest-api/currencies/get-a-currency" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<CoinbaseExAsset[]>> GetAssetsAsync(CancellationToken ct = default);


    /// <summary>
    /// Get the list of supported trading pairs
    /// <para><a href="https://docs.cdp.coinbase.com/api-reference/exchange-api/rest-api/products/get-all-known-trading-pairs" /></para>
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns></returns>
    Task<WebCallResult<CoinbaseExSymbol[]>> GetSymbolsAsync(CancellationToken ct = default);
}