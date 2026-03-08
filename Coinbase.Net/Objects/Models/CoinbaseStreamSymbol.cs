using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record CoinbaseStreamSymbol
    {
        /// <summary>
        /// ["<c>product_type</c>"] Symbol type
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_currency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_currency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_increment</c>"] Base increment
        /// </summary>
        [JsonPropertyName("base_increment")]
        public decimal QuantityStep { get; set; }
        /// <summary>
        /// ["<c>quote_increment</c>"] Quote quantity increment
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public decimal QuoteQuantityStep { get; set; }
        /// <summary>
        /// ["<c>display_name</c>"] Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus SymbolStatus { get; set; }
        /// <summary>
        /// ["<c>status_message</c>"] Status message
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// ["<c>min_market_funds</c>"] Min notional value
        /// </summary>
        [JsonPropertyName("min_market_funds")]
        public decimal MinNotionalValue { get; set; }
    }


}
