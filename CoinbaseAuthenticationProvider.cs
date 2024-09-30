using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Xml.Linq;
using Coinbase.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Jose;

namespace Coinbase.Net
{
    internal class CoinbaseAuthenticationProvider : AuthenticationProvider
    {
        private static IMessageSerializer _serializer = new SystemTextJsonMessageSerializer();

        public CoinbaseAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            headers = new Dictionary<string, string>() { };

            if (!auth)
                return;

            var lines = _credentials.Secret.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var strippedKey = string.Join("", lines.Skip(1).Take(lines.Length - 2));

#if NETSTANDARD2_1_OR_GREATER
            using var key = ECDsa.Create();
            key.ImportECPrivateKey(Convert.FromBase64String(strippedKey), out _);

            var timestamp = GetTimestamp(apiClient);
            var payload = new Dictionary<string, object>
             {
                 { "sub", ApiKey },
                 { "iss", "coinbase-cloud" },
                 { "nbf", (long)DateTimeConverter.ConvertToSeconds(timestamp) },
                 { "exp", (long)DateTimeConverter.ConvertToSeconds(timestamp.AddMinutes(1)) },
                 { "uri", $"{method} {uri.Host}{uri.AbsolutePath}" }
             };

            var extraHeaders = new Dictionary<string, object>
             {
                 { "kid", ApiKey },
                 { "nonce", ExchangeHelpers.RandomString(10) },
                 { "typ", "JWT"}
             };

            headers.Add("Authorization", $"Bearer {JWT.Encode(payload, key, JwsAlgorithm.ES256, extraHeaders)}");
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}
