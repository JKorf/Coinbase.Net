using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePorfolioBreakdownWrapper
    {
        /// <summary>
        /// ["<c>breakdown</c>"] Breakdown
        /// </summary>
        [JsonPropertyName("breakdown")]
        public CoinbasePorfolioBreakdown Breakdown { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbasePorfolioBreakdown
    {
        /// <summary>
        /// ["<c>portfolio</c>"] Portfolio
        /// </summary>
        [JsonPropertyName("portfolio")]
        public CoinbasePortfolio Portfolio { get; set; } = null!;
        /// <summary>
        /// ["<c>portfolio_balances</c>"] Portfolio balances
        /// </summary>
        [JsonPropertyName("portfolio_balances")]
        public CoinbasePorfolioBreakdownBalances PortfolioBalances { get; set; } = null!;
        /// <summary>
        /// ["<c>spot_positions</c>"] Spot balances
        /// </summary>
        [JsonPropertyName("spot_positions")]
        public CoinbasePorfolioSpotBalance[] SpotBalances { get; set; } = Array.Empty<CoinbasePorfolioSpotBalance>();
        /// <summary>
        /// ["<c>perp_positions</c>"] Perpetual futures positions
        /// </summary>
        [JsonPropertyName("perp_positions")]
        public CoinbasePorfolioBreakdownPosition[] PerpetualPositions { get; set; } = Array.Empty<CoinbasePorfolioBreakdownPosition>();
        /// <summary>
        /// ["<c>futures_positions</c>"] Delivery futures positions
        /// </summary>
        [JsonPropertyName("futures_positions")]
        public CoinbasePorfolioBreakdownFuturesPosition[] FuturesPositions { get; set; } = Array.Empty<CoinbasePorfolioBreakdownFuturesPosition>();
    }

    /// <summary>
    /// Total balances
    /// </summary>
    [SerializationModel]
    public record CoinbasePorfolioBreakdownBalances
    {
        /// <summary>
        /// ["<c>total_balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public CoinbaseQuantityReference TotalBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>total_futures_balance</c>"] Total futures balance
        /// </summary>
        [JsonPropertyName("total_futures_balance")]
        public CoinbaseQuantityReference TotalFuturesBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>total_cash_equivalent_balance</c>"] Total cash equivalent balance
        /// </summary>
        [JsonPropertyName("total_cash_equivalent_balance")]
        public CoinbaseQuantityReference TotalCashEquivalentBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>total_crypto_balance</c>"] Total crypto balance
        /// </summary>
        [JsonPropertyName("total_crypto_balance")]
        public CoinbaseQuantityReference TotalCryptoBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>futures_unrealized_pnl</c>"] Delivery futures unrealized profit and loss
        /// </summary>
        [JsonPropertyName("futures_unrealized_pnl")]
        public CoinbaseQuantityReference FuturesUnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>perp_unrealized_pnl</c>"] Perp futures unrealized profit and loss
        /// </summary>
        [JsonPropertyName("perp_unrealized_pnl")]
        public CoinbaseQuantityReference PerpUnrealizedPnl { get; set; } = null!;
    }

    /// <summary>
    /// Spot balances
    /// </summary>
    [SerializationModel]
    public record CoinbasePorfolioSpotBalance
    {
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>account_uuid</c>"] Account uuid
        /// </summary>
        [JsonPropertyName("account_uuid")]
        public string AccountUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total_balance_fiat</c>"] Total balance fiat
        /// </summary>
        [JsonPropertyName("total_balance_fiat")]
        public decimal TotalBalanceFiat { get; set; }
        /// <summary>
        /// ["<c>total_balance_crypto</c>"] Total balance crypto
        /// </summary>
        [JsonPropertyName("total_balance_crypto")]
        public decimal TotalBalanceCrypto { get; set; }
        /// <summary>
        /// ["<c>available_to_trade_fiat</c>"] Available to trade fiat
        /// </summary>
        [JsonPropertyName("available_to_trade_fiat")]
        public decimal AvailableToTradeFiat { get; set; }
        /// <summary>
        /// ["<c>allocation</c>"] Allocation
        /// </summary>
        [JsonPropertyName("allocation")]
        public decimal Allocation { get; set; }
        /// <summary>
        /// ["<c>one_day_change</c>"] One day change
        /// </summary>
        [JsonPropertyName("one_day_change")]
        public decimal OneDayChange { get; set; }
        /// <summary>
        /// ["<c>cost_basis</c>"] Cost basis
        /// </summary>
        [JsonPropertyName("cost_basis")]
        public CoinbaseQuantityReference CostBasis { get; set; } = null!;
        /// <summary>
        /// ["<c>asset_img_url</c>"] Asset image url
        /// </summary>
        [JsonPropertyName("asset_img_url")]
        public string AssetImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>is_cash</c>"] Is cash
        /// </summary>
        [JsonPropertyName("is_cash")]
        public bool IsCash { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record CoinbasePorfolioBreakdownPosition
    {
        /// <summary>
        /// ["<c>product_id</c>"] Product id
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_uuid</c>"] Product uuid
        /// </summary>
        [JsonPropertyName("product_uuid")]
        public string ProductUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset_image_url</c>"] Asset image url
        /// </summary>
        [JsonPropertyName("asset_image_url")]
        public string AssetImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>vwap</c>"] Vwap
        /// </summary>
        [JsonPropertyName("vwap")]
        public CoinbaseCurrencyQuantityReference Vwap { get; set; } = null!;
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// ["<c>net_size</c>"] Net quantity
        /// </summary>
        [JsonPropertyName("net_size")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// ["<c>buy_order_size</c>"] Buy order quantity
        /// </summary>
        [JsonPropertyName("buy_order_size")]
        public decimal BuyOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>sell_order_size</c>"] Sell order quantity
        /// </summary>
        [JsonPropertyName("sell_order_size")]
        public decimal SellOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>im_contribution</c>"] Initial margin contribution
        /// </summary>
        [JsonPropertyName("im_contribution")]
        public decimal InitialMarginContribution { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseCurrencyQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public CoinbaseCurrencyQuantityReference MarkPrice { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public CoinbaseCurrencyQuantityReference LiquidationPrice { get; set; } = null!;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>im_notional</c>"] Initial margin notional
        /// </summary>
        [JsonPropertyName("im_notional")]
        public CoinbaseCurrencyQuantityReference InitialMarginNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>mm_notional</c>"] Maintenance margin notional
        /// </summary>
        [JsonPropertyName("mm_notional")]
        public CoinbaseCurrencyQuantityReference MaintenanceMarginNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>position_notional</c>"] Position notional
        /// </summary>
        [JsonPropertyName("position_notional")]
        public CoinbaseCurrencyQuantityReference PositionNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>margin_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer</c>"] Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal LiquidationBuffer { get; set; }
        /// <summary>
        /// ["<c>liquidation_percentage</c>"] Liquidation percentage
        /// </summary>
        [JsonPropertyName("liquidation_percentage")]
        public decimal LiquidationPercentage { get; set; }
    }

    /// <summary>
    /// Futures position info
    /// </summary>
    [SerializationModel]
    public record CoinbasePorfolioBreakdownFuturesPosition
    {
        /// <summary>
        /// ["<c>product_id</c>"] Product id
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_size</c>"] Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? Side { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>avg_entry_price</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("avg_entry_price")]
        public decimal? AverageEntryPrice { get; set; }
        /// <summary>
        /// ["<c>current_price</c>"] Current price
        /// </summary>
        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized pnl
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>expiry</c>"] Expiry
        /// </summary>
        [JsonPropertyName("expiry")]
        public DateTime Expiry { get; set; }
        /// <summary>
        /// ["<c>underlying_asset</c>"] Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_asset")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset_img_url</c>"] Asset img url
        /// </summary>
        [JsonPropertyName("asset_img_url")]
        public string AssetImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_name</c>"] Product name
        /// </summary>
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>venue</c>"] Venue
        /// </summary>
        [JsonPropertyName("venue")]
        public string Venue { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>notional_value</c>"] Notional value
        /// </summary>
        [JsonPropertyName("notional_value")]
        public decimal NotionalValue { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbaseCurrencyQuantityReference
    {
        /// <summary>
        /// ["<c>userNativeCurrency</c>"] User native asset
        /// </summary>
        [JsonPropertyName("userNativeCurrency")]
        public CoinbaseQuantityReference UserNativeAsset { get; set; } = null!;
        /// <summary>
        /// ["<c>rawCurrency</c>"] Raw asset
        /// </summary>
        [JsonPropertyName("rawCurrency")]
        public CoinbaseQuantityReference RawAsset { get; set; } = null!;
    }
}
