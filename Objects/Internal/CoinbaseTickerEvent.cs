using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseTickerEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("tickers")]
        public IEnumerable<CoinbaseTicker> Tickers { get; set; }
    }
}
