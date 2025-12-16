using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;

namespace Coinbase.Net.Objects.Options
{
    /// <summary>
    /// Coinbase options
    /// </summary>
    public class CoinbaseOptions : LibraryOptions<CoinbaseRestOptions, CoinbaseSocketOptions, ApiCredentials, CoinbaseEnvironment>
    {
    }
}
