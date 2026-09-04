using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Coinbase
    /// </summary>
    public interface ICoinbaseSharedApiClient
    {
        /// <summary>
        /// REST shared API implementations
        /// </summary>
        ICoinbaseRestClientAdvancedTradeSharedApi Rest { get; }

        /// <summary>
        /// WebSocket shared API implementations
        /// </summary>
        ICoinbaseSocketClientAdvancedTradeSharedApi Socket { get; }
    }
}
