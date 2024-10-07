using CryptoExchange.Net.Interfaces;
using Coinbase.Net.Clients;
using Coinbase.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Coinbase REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static ICoinbaseRestClient Coinbase(this ICryptoRestClient baseClient) => baseClient.TryGet<ICoinbaseRestClient>(() => new CoinbaseRestClient());

        /// <summary>
        /// Get the Coinbase Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static ICoinbaseSocketClient Coinbase(this ICryptoSocketClient baseClient) => baseClient.TryGet<ICoinbaseSocketClient>(() => new CoinbaseSocketClient());
    }
}
