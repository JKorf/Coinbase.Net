using CryptoExchange.Net.Authentication;
using System;

namespace Coinbase.Net
{
    /// <summary>
    /// Coinbase API credentials
    /// </summary>
    public class CoinbaseCredentials : ECDsaCredential
    {
        /// <summary>
        /// Create new credentials
        /// </summary>
        public CoinbaseCredentials() { }

        /// <summary>
        /// Create new credentials providing only credentials in ECDsa format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public CoinbaseCredentials(string key, string privateKey) : base(key, privateKey)
        {
        }

        /// <summary>
        /// Create new credentials providing ECDsa credentials
        /// </summary>
        /// <param name="credential">ECDsa credentials</param>
        public CoinbaseCredentials(ECDsaCredential credential) : base(credential.Key, credential.PrivateKey)
        {
        }

        /// <summary>
        /// Specify the ECDsa credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="privateKey">Private key</param>
        public CoinbaseCredentials WithECDsa(string key, string privateKey)
        {
            if (!string.IsNullOrEmpty(Key)) throw new InvalidOperationException("Credentials already set");

            Key = key;
            PrivateKey = privateKey;
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new CoinbaseCredentials(this);
    }
}
