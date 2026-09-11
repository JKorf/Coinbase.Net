using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Coinbase options
    /// </summary>
    public class CoinbaseOptions : LibraryOptions<CoinbaseRestOptions, CoinbaseSocketOptions, CoinbaseCredentials, CoinbaseEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
