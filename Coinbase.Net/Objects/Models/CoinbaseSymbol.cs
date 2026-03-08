using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseSymbolWrapper
    {
        [JsonPropertyName("products")]
        public CoinbaseSymbol[] Symbols { get; set; } = Array.Empty<CoinbaseSymbol>();
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record CoinbaseSymbol
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Last price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>price_percentage_change_24h</c>"] Price percentage change in last 24 hours
        /// </summary>
        [JsonPropertyName("price_percentage_change_24h")]
        public decimal? PricePercentageChange24h { get; set; }
        /// <summary>
        /// ["<c>volume_24h</c>"] Volume in base asset in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal? Volume24h { get; set; }
        /// <summary>
        /// ["<c>volume_percentage_change_24h</c>"] Volume percentage change in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_percentage_change_24h")]
        public decimal? VolumePercentageChange24h { get; set; }
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
        /// ["<c>quote_min_size</c>"] Quote min quantity
        /// </summary>
        [JsonPropertyName("quote_min_size")]
        public decimal QuoteMinQuantity { get; set; }
        /// <summary>
        /// ["<c>quote_max_size</c>"] Quote max quantity
        /// </summary>
        [JsonPropertyName("quote_max_size")]
        public decimal QuoteMaxQuantity { get; set; }
        /// <summary>
        /// ["<c>base_min_size</c>"] Min order quantity
        /// </summary>
        [JsonPropertyName("base_min_size")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>base_max_size</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("base_max_size")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>base_name</c>"] Base asset name
        /// </summary>
        [JsonPropertyName("base_name")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_name</c>"] Quote asset name
        /// </summary>
        [JsonPropertyName("quote_name")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>watched</c>"] Watched
        /// </summary>
        [JsonPropertyName("watched")]
        public bool Watched { get; set; }
        /// <summary>
        /// ["<c>is_disabled</c>"] Is disabled
        /// </summary>
        [JsonPropertyName("is_disabled")]
        public bool IsDisabled { get; set; }
        /// <summary>
        /// ["<c>new</c>"] Whether the symbol is 'new'
        /// </summary>
        [JsonPropertyName("new")]
        public bool New { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the symbol
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus? SymbolStatus { get; set; }
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
        /// ["<c>trading_disabled</c>"] Trading disabled
        /// </summary>
        [JsonPropertyName("trading_disabled")]
        public bool TradingDisabled { get; set; }
        /// <summary>
        /// ["<c>auction_mode</c>"] Whether or not the product is in auction mode.
        /// </summary>
        [JsonPropertyName("auction_mode")]
        public bool AuctionMode { get; set; }
        /// <summary>
        /// ["<c>product_type</c>"] Type of symbol
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// ["<c>quote_currency_id</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote_currency_id")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_currency_id</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_currency_id")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fcm_trading_session_details</c>"] Fcm session details
        /// </summary>
        [JsonPropertyName("fcm_trading_session_details")]
        public CoinbaseSymbolFcmInfo FcmSession { get; set; } = null!;
        /// <summary>
        /// ["<c>mid_market_price</c>"] Mid market price
        /// </summary>
        [JsonPropertyName("mid_market_price")]
        public decimal? MidMarketPrice { get; set; }
        /// <summary>
        /// ["<c>alias</c>"] Alias
        /// </summary>
        [JsonPropertyName("alias")]
        public string Alias { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>alias_to</c>"] Alias to
        /// </summary>
        [JsonPropertyName("alias_to")]
        public string[] AliasTo { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>base_display_symbol</c>"] Base display symbol
        /// </summary>
        [JsonPropertyName("base_display_symbol")]
        public string BaseDisplaySymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quote_display_symbol</c>"] Quote display symbol
        /// </summary>
        [JsonPropertyName("quote_display_symbol")]
        public string QuoteDisplaySymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>view_only</c>"] Reflects whether an FCM product has expired.
        /// </summary>
        [JsonPropertyName("view_only")]
        public bool ViewOnly { get; set; }
        /// <summary>
        /// ["<c>price_increment</c>"] Price step
        /// </summary>
        [JsonPropertyName("price_increment")]
        public decimal PriceStep { get; set; }
        /// <summary>
        /// ["<c>display_name</c>"] Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_venue</c>"] Product venue
        /// </summary>
        [JsonPropertyName("product_venue")]
        public string ProductVenue { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>approximate_quote_24h_volume</c>"] Approximate quote 24h volume
        /// </summary>
        [JsonPropertyName("approximate_quote_24h_volume")]
        public decimal? ApproximateQuote24hVolume { get; set; }
        /// <summary>
        /// ["<c>future_product_details</c>"] Future product details
        /// </summary>
        [JsonPropertyName("future_product_details")]
        public CoinbaseSymbolFuturesDetails? FutureProductDetails { get; set; } = null!;
    }

    /// <summary>
    /// FCM info
    /// </summary>
    [SerializationModel]
    public record CoinbaseSymbolFcmInfo
    {
        /// <summary>
        /// ["<c>is_session_open</c>"] Is session open
        /// </summary>
        [JsonPropertyName("is_session_open")]
        public bool IsSessionOpen { get; set; }
        /// <summary>
        /// ["<c>open_time</c>"] Open time
        /// </summary>
        [JsonPropertyName("open_time")]
        public DateTime? OpenTime { get; set; }
        /// <summary>
        /// ["<c>close_time</c>"] Close time
        /// </summary>
        [JsonPropertyName("close_time")]
        public DateTime? CloseTime { get; set; }
        /// <summary>
        /// ["<c>session_state</c>"] Session state
        /// </summary>
        [JsonPropertyName("session_state")]
        public string SessionState { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>after_hours_order_entry_disabled</c>"] After hours order entry disabled
        /// </summary>
        [JsonPropertyName("after_hours_order_entry_disabled")]
        public bool AfterHoursOrderEntryDisabled { get; set; }
    }

    /// <summary>
    /// Futures details
    /// </summary>
    [SerializationModel]
    public record CoinbaseSymbolFuturesDetails
    {
        /// <summary>
        /// ["<c>venue</c>"] Venue
        /// </summary>
        [JsonPropertyName("venue")]
        public string Venue { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_expiry</c>"] Contract expiry
        /// </summary>
        [JsonPropertyName("contract_expiry")]
        public DateTime? ContractExpiry { get; set; }
        /// <summary>
        /// ["<c>contract_size</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>contract_root_unit</c>"] Contract settlement asset
        /// </summary>
        [JsonPropertyName("contract_root_unit")]
        public string ContractSettlementAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>group_description</c>"] Group description
        /// </summary>
        [JsonPropertyName("group_description")]
        public string GroupDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_expiry_timezone</c>"] Contract expiry timezone
        /// </summary>
        [JsonPropertyName("contract_expiry_timezone")]
        public string ContractExpiryTimezone { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>group_short_description</c>"] Group short description
        /// </summary>
        [JsonPropertyName("group_short_description")]
        public string GroupShortDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>risk_managed_by</c>"] Risk managed by
        /// </summary>
        [JsonPropertyName("risk_managed_by")]
        public RiskManageType RiskManagedBy { get; set; }
        /// <summary>
        /// ["<c>contract_expiry_type</c>"] Contract expiry type
        /// </summary>
        [JsonPropertyName("contract_expiry_type")]
        public ContractExpiryType ContractExpiryType { get; set; }
        /// <summary>
        /// ["<c>perpetual_details</c>"] Perpetual details
        /// </summary>
        [JsonPropertyName("perpetual_details")]
        public CoinbaseSymbolPerpDetails? PerpetualDetails { get; set; } = null!;
        /// <summary>
        /// ["<c>contract_display_name</c>"] Contract display name
        /// </summary>
        [JsonPropertyName("contract_display_name")]
        public string ContractDisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>time_to_expiry_ms</c>"] Time to expiry ms
        /// </summary>
        [JsonPropertyName("time_to_expiry_ms")]
        public long? TimeToExpiryMs { get; set; }
        /// <summary>
        /// ["<c>non_crypto</c>"] Non crypto
        /// </summary>
        [JsonPropertyName("non_crypto")]
        public bool NonCrypto { get; set; }
        /// <summary>
        /// ["<c>contract_expiry_name</c>"] Contract expiry name
        /// </summary>
        [JsonPropertyName("contract_expiry_name")]
        public string ContractExpiryName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Perpetual info
    /// </summary>
    [SerializationModel]
    public record CoinbaseSymbolPerpDetails
    {
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal? OpenInterest { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>max_leverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("max_leverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>base_asset_uuid</c>"] Base asset uuid
        /// </summary>
        [JsonPropertyName("base_asset_uuid")]
        public string BaseAssetUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlying_type</c>"] Underlying type
        /// </summary>
        [JsonPropertyName("underlying_type")]
        public string? UnderlyingType { get; set; }
    }
}
