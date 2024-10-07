using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseTradeEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("trades")]
        public IEnumerable<CoinbaseTrade> Trades { get; set; } = Array.Empty<CoinbaseTrade>();
    }
}
