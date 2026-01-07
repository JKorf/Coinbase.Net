namespace Coinbase.Net.Interfaces.Clients.ExchangeApi;

/// <summary>
/// Coinbase exchange rest api endpoints.
/// </summary>
public interface ICoinbaseRestClientExchangeApi
{
    /// <summary>
    /// Endpoints related to retrieving market and system data, specifically for the Exchange API
    /// </summary>
    /// <see cref="ICoinbaseRestClientExchangeApiExchangeData"/>
    ICoinbaseRestClientExchangeApiExchangeData ExchangeData { get; }
}