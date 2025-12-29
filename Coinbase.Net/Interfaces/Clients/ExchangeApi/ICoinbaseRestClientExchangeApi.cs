namespace Coinbase.Net.Interfaces.Clients.ExchangeApi;

/// <summary>
/// Coinbase exchange rest api endpoints.
/// </summary>
public interface ICoinbaseRestClientExchangeApi
{
    /// <summary>
    /// Endpoints related to retrieving market and system data
    /// </summary>
    /// <see cref="ICoinbaseRestClientExchangeApiExchangeData"/>
    ICoinbaseRestClientExchangeApiExchangeData ExchangeData { get; }
}