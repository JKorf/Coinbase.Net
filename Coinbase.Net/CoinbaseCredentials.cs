using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net
{
    /// <summary>
    /// Coinbase credentials
    /// </summary>
    public class CoinbaseCredentials : ApiCredentials
    {
        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public CoinbaseCredentials() { }

        /// <summary>
        /// Create credentials using an ECDsa key and secret.
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public CoinbaseCredentials(string apiKey, string secret) : this(new ECDsaCredential(apiKey, secret))
        {
        }

        /// <summary>
        /// Create Coinbase credentials using ECDsa credentials
        /// </summary>
        /// <param name="credential">The ECDsa credentials</param>
        public CoinbaseCredentials(ECDsaCredential credential) : base(credential)
        {
        }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new CoinbaseCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
