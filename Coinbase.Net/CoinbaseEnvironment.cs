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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public CoinbaseEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Coinbase environment by name
        /// </summary>
        public static CoinbaseEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

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
