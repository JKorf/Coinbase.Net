using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseKlineEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("candles")]
        public CoinbaseStreamKline[] Klines { get; set; } = Array.Empty<CoinbaseStreamKline>();
    }
}
