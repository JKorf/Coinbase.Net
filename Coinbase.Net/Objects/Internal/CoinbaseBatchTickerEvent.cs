using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Models;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseBatchTickerEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("tickers")]
        public CoinbaseBatchTicker[] Tickers { get; set; } = Array.Empty<CoinbaseBatchTicker>();
    }
}
