using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Models;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseTradeEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("trades")]
        public CoinbaseTrade[] Trades { get; set; } = Array.Empty<CoinbaseTrade>();
    }
}
