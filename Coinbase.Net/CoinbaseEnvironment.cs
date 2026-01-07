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
        /// Rest Exchange API address
        /// </summary>
        public string ExchangeRestClientAddress { get; }

        /// <summary>
        /// Socket API address for the Advanced Trade API
        /// </summary>
        public string SocketClientPublicAddress { get; }

        /// <summary>
        /// Socket API address for the Exchange API
        /// </summary>
        public string SocketClientPublicExchangeApiAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SocketClientPrivateAddress { get; }

        internal CoinbaseEnvironment(
            string name,
            string restAddress,
            string exchangeRestAddress,
            string streamAddressPublic,
            string streamAddressPrivate,
            string socketClientPublicExchangeApiAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            ExchangeRestClientAddress = exchangeRestAddress;
            SocketClientPublicAddress = streamAddressPublic;
            SocketClientPrivateAddress = streamAddressPrivate;
            SocketClientPublicExchangeApiAddress = socketClientPublicExchangeApiAddress;
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
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static CoinbaseEnvironment Live { get; }
            = new CoinbaseEnvironment(TradeEnvironmentNames.Live,
                                     CoinbaseApiAddresses.Default.RestClientAddress,
                                     CoinbaseApiAddresses.Default.ExchangeRestClientAddress,
                                     CoinbaseApiAddresses.Default.SocketClientPublicAddress,
                                     CoinbaseApiAddresses.Default.SocketClientPrivateAddress,
                                     CoinbaseApiAddresses.Default.SocketExchangeApiPublicAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static CoinbaseEnvironment CreateCustom(
                        string name,
                        string restAddress,
                        string exchangeRestAddress,
                        string socketStreamPublicAddress,
                        string socketStreamPrivateAddress,
                        string socketClientPublicExchangeApiAddress)
            => new CoinbaseEnvironment(name, restAddress, exchangeRestAddress, socketStreamPublicAddress, socketStreamPrivateAddress, socketClientPublicExchangeApiAddress);
    }
}
