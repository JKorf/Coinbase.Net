using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
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
        /// ["<c>id</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>display_name</c>"] Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
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
        /// ["<c>min_market_funds</c>"] Min order value
        /// </summary>
        [JsonPropertyName("min_market_funds")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// ["<c>base_currency</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_currency</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string QuoteAssetName { get; set; } = string.Empty;       
        
        /// <summary>
        /// ["<c>status</c>"] Status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus? SymbolStatus { get; set; }
        /// <summary>
        /// ["<c>status_message</c>"] Status info
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// ["<c>cancel_only</c>"] Is symbol in cancel only mode
        /// </summary>
        [JsonPropertyName("cancel_only")]
        public bool CancelOnly { get; set; }
        /// <summary>
        /// ["<c>limit_only</c>"] Is symbol in limit order only mode
        /// </summary>
        [JsonPropertyName("limit_only")]
        public bool LimitOnly { get; set; }
        /// <summary>
        /// ["<c>post_only</c>"] Is symbol in post only mode
        /// </summary>
        [JsonPropertyName("post_only")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// ["<c>fx_stablecoin</c>"] Is symbol stablecoin
        /// </summary>
        [JsonPropertyName("fx_stablecoin")]
        public bool FxStablecoin { get; set; }
        /// <summary>
        /// ["<c>margin_enabled</c>"] Is margin enabled
        /// </summary>
        [JsonPropertyName("margin_enabled")]
        public bool MarginEnabled { get; set; }
        /// <summary>
        /// ["<c>trading_disabled</c>"] Is trading disabled
        /// </summary>
        [JsonPropertyName("trading_disabled")]
        public bool TradingDisabled { get; set; }
        /// <summary>
        /// ["<c>max_slippage_percentage</c>"] Max slippage percentage
        /// </summary>
        [JsonPropertyName("max_slippage_percentage")]
        public decimal? MaxSlippagePercentage { get; set; }
        /// <summary>
        /// ["<c>auction_mode</c>"] Is action mode enabled
        /// </summary>
        [JsonPropertyName("auction_mode")]
        public bool AuctionMode { get; set; }
        /// <summary>
        /// ["<c>high_bid_limit_percentage</c>"] Percentage to calculate highest price for limit buy order (Stable coin trading pair only)
        /// </summary>
        [JsonPropertyName("high_bid_limit_percentage")]
        public decimal? HighBidLimitPercentage { get; set; }
    }

}
