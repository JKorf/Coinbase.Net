using CryptoExchange.Net.Objects;
using Coinbase.Net.Objects;

namespace Coinbase.Net
{
    /// <summary>
    /// Coinbase environments
    /// </summary>
    public class CoinbaseEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API address
        /// </summary>
        public string RestClientAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SocketClientPublicAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SocketClientPrivateAddress { get; }

        internal CoinbaseEnvironment(
            string name,
            string restAddress,
            string streamAddressPublic,
            string streamAddressPrivate) :
            base(name)
        {
            RestClientAddress = restAddress;
            SocketClientPublicAddress = streamAddressPublic;
            SocketClientPrivateAddress = streamAddressPrivate;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static CoinbaseEnvironment Live { get; }
            = new CoinbaseEnvironment(TradeEnvironmentNames.Live,
                                     CoinbaseApiAddresses.Default.RestClientAddress,
                                     CoinbaseApiAddresses.Default.SocketClientPublicAddress,
                                     CoinbaseApiAddresses.Default.SocketClientPrivateAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="restAddress"></param>
        /// <param name="socketStreamPublicAddress"></param>
        /// <param name="socketStreamPrivateAddress"></param>
        /// <returns></returns>
        public static CoinbaseEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string socketStreamPublicAddress,
                        string socketStreamPrivateAddress)
            => new CoinbaseEnvironment(name, restAddress, socketStreamPublicAddress, socketStreamPrivateAddress);
    }
}
