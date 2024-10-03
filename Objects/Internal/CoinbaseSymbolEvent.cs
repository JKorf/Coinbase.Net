using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseSymbolEvent : CoinbaseSocketEvent
    {
        [JsonPropertyName("products")]
        public IEnumerable<CoinbaseStreamSymbol> Symbols { get; set; }
    }
}
