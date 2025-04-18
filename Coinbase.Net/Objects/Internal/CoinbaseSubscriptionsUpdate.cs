using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseSubscriptionsUpdate
    {
        [JsonPropertyName("subscriptions")]
        public Dictionary<string, string[]> Subscriptions { get; set; } = new Dictionary<string, string[]>();

    }
}
