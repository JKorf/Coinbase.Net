using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseBatchTickerEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("tickers")]
        public IEnumerable<CoinbaseBatchTicker> Tickers { get; set; } = Array.Empty<CoinbaseBatchTicker>();
    }
}
