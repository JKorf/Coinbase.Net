using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects
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
        /// Create credentials using an Ecdsa key and secret.
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public CoinbaseCredentials(string apiKey, string secret) : this(new ECDSACredential(apiKey, secret))
        {
        }

        /// <summary>
        /// Create Coinbase credentials using Ecdsa credentials
        /// </summary>
        /// <param name="credential">The Ecdsa credentials</param>
        public CoinbaseCredentials(ECDSACredential credential) : base(credential)
        {
        }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new CoinbaseCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
