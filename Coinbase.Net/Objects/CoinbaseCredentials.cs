using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net.Objects
{
    public class CoinbaseCredentials : ApiCredentials
    {
        public CoinbaseCredentials(string apiKey, string secret) : this(new ECDSACredential(apiKey, secret))
        {
        }

        public CoinbaseCredentials(ECDSACredential credential) : base(credential)
        {
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() =>
            new CoinbaseCredentials(GetCredential<ECDSACredential>());
    }
}
