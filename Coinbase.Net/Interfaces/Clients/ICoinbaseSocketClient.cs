using Coinbase.Net.Interfaces.Clients.AdvancedTradeApi;
using Coinbase.Net.Interfaces.Clients.ExchangeApi;
using Coinbase.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Coinbase websocket API
    /// </summary>
    public interface ICoinbaseSocketClient : ISocketClient<CoinbaseCredentials>
    {
        /// <summary>
        /// Advanced trade API streams (Consumer API)
        /// </summary>
        /// <see cref="ICoinbaseSocketClientAdvancedTradeApi"/>
        public ICoinbaseSocketClientAdvancedTradeApi AdvancedTradeApi { get; }
        /// <summary>
        /// Exchange API streams (Institutional API)
        /// </summary>
        public ICoinbaseSocketClientExchangeApi ExchangeApi { get; }
    }
}
