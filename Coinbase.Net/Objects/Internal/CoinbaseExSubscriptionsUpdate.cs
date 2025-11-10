using CryptoExchange.Net.Converters.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseExSubscriptionsUpdate
    {
        [JsonPropertyName("channels")]
        public CoinbaseExSubscriptionItem[] Subscriptions { get; set; } = [];

    }

    internal record CoinbaseExSubscriptionItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("product_ids")]
        public string[] Symbols { get; set; } = [];
    }
}
