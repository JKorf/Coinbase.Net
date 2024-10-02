using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal class CoinbaseTradeEvent
    {
        [JsonPropertyName("type")]
        public string EventType { get; set; }
        [JsonPropertyName("trades")]
        public IEnumerable<CoinbaseTrade> Trades { get; set; }
    }
}
