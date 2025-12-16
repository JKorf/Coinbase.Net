using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
#if !NETSTANDARD2_0
using System.Linq;
using System.Security.Cryptography;
using Microsoft.IdentityModel.JsonWebTokens;
#endif

namespace Coinbase.Net
{
    internal class CoinbaseAuthenticationProvider : AuthenticationProvider
    {
#if !NETSTANDARD2_0
        private SigningCredentials? _signingCreds;
#endif

        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac];
        public CoinbaseAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetTimestamp(apiClient);

            var host = request.BaseAddress.Substring(request.BaseAddress.IndexOf("//") + 2);
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("Authorization", $"Bearer {GenerateToken(timestamp, $"{request.Method} {host}{request.Path}")}");
        }

        public string GenerateToken(DateTime timestamp, string? uriLine)
        {
#if !NETSTANDARD2_0
            var nonce = new byte[16];
            RandomNumberGenerator.Fill(nonce);

            if (_signingCreds == null)
            {
                var lines = _credentials.Secret.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                var strippedKey = string.Join("", lines.Skip(1).Take(lines.Length - 2));

                var key = ECDsa.Create();
                key.ImportECPrivateKey(Convert.FromBase64String(strippedKey), out _);
                _signingCreds = new SigningCredentials(new ECDsaSecurityKey(key) { KeyId = ApiKey }, "ES256");
            }

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = "coinbase-cloud",
                NotBefore = timestamp,
                Expires = timestamp.AddMinutes(1),
                Claims = new Dictionary<string, object> {
                    { "sub", ApiKey }
                },
                AdditionalHeaderClaims = new Dictionary<string, object>
                {
                    { "nonce", BytesToHexString(nonce) },
                    { "typ", "JWT" }
                },
                SigningCredentials = _signingCreds
            };

            if (uriLine != null)
                descriptor.Claims.Add("uri", uriLine);

            var result = new JsonWebTokenHandler().CreateToken(descriptor);
            return result;
#else
            throw new PlatformNotSupportedException("Authentication is not available for .NetStandard2.0 due to platform limitations");
#endif
        }
    }

}
