using CryptoExchange.Net.Authentication;

namespace Coinbase.Net
{
    /// <summary>
    /// Coinbase API credentials
    /// </summary>
    public class CoinbaseCredentials : ECDsaCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in ECDsa format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public CoinbaseCredentials(string key, string privateKey) : base(key, privateKey)
        {
        }
    }
}
