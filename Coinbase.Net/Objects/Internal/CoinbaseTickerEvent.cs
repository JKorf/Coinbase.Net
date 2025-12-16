using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Models;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseTickerEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("tickers")]
        public CoinbaseTicker[] Tickers { get; set; } = Array.Empty<CoinbaseTicker>();
    }
}
