using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    internal record CoinbaseSubscriptionsUpdate
    {
        [JsonPropertyName("subscriptions")]
        public Dictionary<string, IEnumerable<string>> Subscriptions { get; set; } = new Dictionary<string, IEnumerable<string>>();

    }
}
