using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseKlineEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("candles")]
        public IEnumerable<CoinbaseStreamKline> Klines { get; set; } = Array.Empty<CoinbaseStreamKline>();
    }
}
