using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
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
        private static IStringMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CoinbaseExchange._serializerContext));
        private static JwtSettings _mapperSettings = new JwtSettings() { JsonMapper = new JwtJsonMapper() };

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

            var timestamp = GetTimestamp(apiClient);

            headers.Add("Authorization", $"Bearer {GenerateToken(timestamp, $"{method} {uri.Host}{uri.AbsolutePath}")}");

        }

        public string GenerateToken(DateTime timestamp, string? uriLine)
        {
#if NETSTANDARD2_1_OR_GREATER || NET8_0_OR_GREATER

            var lines = _credentials.Secret.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var strippedKey = string.Join("", lines.Skip(1).Take(lines.Length - 2));

            using var key = ECDsa.Create();
            key.ImportECPrivateKey(Convert.FromBase64String(strippedKey), out _);

            var payload = new Dictionary<string, object>
             {
                 { "iss", "coinbase-cloud" },
                 { "nbf", (long)DateTimeConverter.ConvertToSeconds(timestamp) },
                 { "exp", (long)DateTimeConverter.ConvertToSeconds(timestamp.AddMinutes(1)) },
                 { "sub", ApiKey },
             };
            if (uriLine != null)
                payload.Add("uri", uriLine);

            var nonce = new byte[16];
            RandomNumberGenerator.Fill(nonce);
            var extraHeaders = new Dictionary<string, object>
             {
                 { "kid", ApiKey },
                 { "nonce", BytesToHexString(nonce) },
             };

            var payloadStr = _serializer.Serialize(payload);
            return JWT.Encode(payloadStr, key, JwsAlgorithm.ES256, extraHeaders, _mapperSettings);
#else
            throw new PlatformNotSupportedException("Authentication is not available for .NetStandard2.0 due to platform limitations");
#endif
        }

        // Override the default to make sure the correct json serializer options are used
        class JwtJsonMapper : IJsonMapper
        {
            public T Parse<T>(string json)
            {
                return JsonSerializer.Deserialize(json, (JsonTypeInfo<T>)CoinbaseExchange._serializerContext.GetTypeInfo(typeof(T))!)!;
            }

            public string Serialize(object obj) => _serializer.Serialize(obj);
        }
    }

}
