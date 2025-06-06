using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Models;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    [SerializationModel]
    internal record CoinbaseFuturesBalanceUpdate : CoinbaseSocketEvent
    {
        [JsonPropertyName("fcm_balance_summary")]
        public CoinbaseFuturesBalance BalanceSummary { get; set; } = null!;
    }
}
