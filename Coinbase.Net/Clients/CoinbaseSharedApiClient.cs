using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc />
    public class CoinbaseSharedApiClient : ICoinbaseSharedApiClient
    {
        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeSharedApi Rest { get; }
        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeSharedApi Socket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseSharedApiClient(
            ICoinbaseRestClient restClient,
            ICoinbaseSocketClient socketClient)
        {
            Rest = restClient.AdvancedTradeApi.SharedApi;
            Socket = socketClient.AdvancedTradeApi.SharedApi;
        }
    }
}
