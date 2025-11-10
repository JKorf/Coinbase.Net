using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record CoinbaseExSymbol
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Base increment
        /// </summary>
        [JsonPropertyName("base_increment")]
        public decimal QuantityStep { get; set; }
        /// <summary>
        /// Quote quantity increment
        /// </summary>
        [JsonPropertyName("quote_increment")]
        public decimal QuoteQuantityStep { get; set; }        
        /// <summary>
        /// Min order value
        /// </summary>
        [JsonPropertyName("min_market_funds")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// Base asset name
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset name
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteAssetName { get; set; } = string.Empty;       
        
        /// <summary>
        /// Status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus? SymbolStatus { get; set; }
        /// <summary>
        /// Status info
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// Is symbol in cancel only mode
        /// </summary>
        [JsonPropertyName("cancel_only")]
        public bool CancelOnly { get; set; }
        /// <summary>
        /// Is symbol in limit order only mode
        /// </summary>
        [JsonPropertyName("limit_only")]
        public bool LimitOnly { get; set; }
        /// <summary>
        /// Is symbol in post only mode
        /// </summary>
        [JsonPropertyName("post_only")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// Is symbol stablecoin
        /// </summary>
        [JsonPropertyName("fx_stablecoin")]
        public bool FxStablecoin { get; set; }

    }

}
