using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using CryptoExchange.Net.SharedApis;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Coinbase
    /// </summary>
    public interface ICoinbaseSharedApiClient : ISharedApiClientBase
    {
        /// <summary>
        /// REST shared API implementations
        /// </summary>
        ICoinbaseRestClientAdvancedTradeSharedApi AdvancedTradeRest { get; }

        /// <summary>
        /// WebSocket shared API implementations
        /// </summary>
        ICoinbaseSocketClientAdvancedTradeSharedApi AdvancedTradeSocket { get; }
    }
}
