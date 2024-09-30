namespace Coinbase.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class CoinbaseApiAddresses
    {
        /// <summary>
        /// The address used by the CoinbaseRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the CoinbaseSocketClient for public data from the websocket API
        /// </summary>
        public string SocketClientPublicAddress { get; set; } = "";
        /// <summary>
        /// The address used by the CoinbaseSocketClient for user data from the websocket API
        /// </summary>
        public string SocketClientPrivateAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Coinbase API
        /// </summary>
        public static CoinbaseApiAddresses Default = new CoinbaseApiAddresses
        {
            RestClientAddress = "https://api.coinbase.com",
            SocketClientPublicAddress = "wss://ws-feed.exchange.coinbase.com",
            SocketClientPrivateAddress = "wss://advanced-trade-ws-user.coinbase.com"
        };
    }
}
