using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace Coinbase.Net.Clients
{
    /// <inheritdoc />
    public class CoinbaseSharedApiClient : SharedApiClientBase, ICoinbaseSharedApiClient
    {
        /// <inheritdoc />
        public ICoinbaseRestClientAdvancedTradeSharedApi AdvancedTradeRest { get; }
        /// <inheritdoc />
        public ICoinbaseSocketClientAdvancedTradeSharedApi AdvancedTradeSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public CoinbaseSharedApiClient(
            ICoinbaseRestClient restClient,
            ICoinbaseSocketClient socketClient,
            IOptions<CoinbaseOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                  restClient.AdvancedTradeApi.SharedApi,
                  socketClient.AdvancedTradeApi.SharedApi
                  )
        {
            AdvancedTradeRest = restClient.AdvancedTradeApi.SharedApi;
            AdvancedTradeSocket = socketClient.AdvancedTradeApi.SharedApi;
        }
    }
}
