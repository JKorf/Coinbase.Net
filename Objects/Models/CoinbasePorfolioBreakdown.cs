using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbasePorfolioBreakdownWrapper
    {
        /// <summary>
        /// Breakdown
        /// </summary>
        [JsonPropertyName("breakdown")]
        public CoinbasePorfolioBreakdown Breakdown { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    public record CoinbasePorfolioBreakdown
    {
        /// <summary>
        /// Portfolio
        /// </summary>
        [JsonPropertyName("portfolio")]
        public CoinbasePortfolio Portfolio { get; set; } = null!;
        /// <summary>
        /// Portfolio balances
        /// </summary>
        [JsonPropertyName("portfolio_balances")]
        public CoinbasePorfolioBreakdownBalances PortfolioBalances { get; set; } = null!;
        /// <summary>
        /// Spot balances
        /// </summary>
        [JsonPropertyName("spot_positions")]
        public IEnumerable<CoinbasePorfolioSpotBalance> SpotBalances { get; set; } = Array.Empty<CoinbasePorfolioSpotBalance>();
        /// <summary>
        /// Perpetual futures positions
        /// </summary>
        [JsonPropertyName("perp_positions")]
        public IEnumerable<CoinbasePorfolioBreakdownPosition> PerpetualPositions { get; set; } = Array.Empty<CoinbasePorfolioBreakdownPosition>();
        /// <summary>
        /// Delivery futures positions
        /// </summary>
        [JsonPropertyName("futures_positions")]
        public IEnumerable<CoinbasePorfolioBreakdownFuturesPosition> FuturesPositions { get; set; } = Array.Empty<CoinbasePorfolioBreakdownFuturesPosition>();
    }

    /// <summary>
    /// Total balances
    /// </summary>
    public record CoinbasePorfolioBreakdownBalances
    {
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public CoinbaseQuantityReference TotalBalance { get; set; } = null!;
        /// <summary>
        /// Total futures balance
        /// </summary>
        [JsonPropertyName("total_futures_balance")]
        public CoinbaseQuantityReference TotalFuturesBalance { get; set; } = null!;
        /// <summary>
        /// Total cash equivalent balance
        /// </summary>
        [JsonPropertyName("total_cash_equivalent_balance")]
        public CoinbaseQuantityReference TotalCashEquivalentBalance { get; set; } = null!;
        /// <summary>
        /// Total crypto balance
        /// </summary>
        [JsonPropertyName("total_crypto_balance")]
        public CoinbaseQuantityReference TotalCryptoBalance { get; set; } = null!;
        /// <summary>
        /// Delivery futures unrealized profit and loss
        /// </summary>
        [JsonPropertyName("futures_unrealized_pnl")]
        public CoinbaseQuantityReference FuturesUnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// Perp futures unrealized profit and loss
        /// </summary>
        [JsonPropertyName("perp_unrealized_pnl")]
        public CoinbaseQuantityReference PerpUnrealizedPnl { get; set; } = null!;
    }

    /// <summary>
    /// Spot balances
    /// </summary>
    public record CoinbasePorfolioSpotBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Account uuid
        /// </summary>
        [JsonPropertyName("account_uuid")]
        public string AccountUuid { get; set; } = string.Empty;
        /// <summary>
        /// Total balance fiat
        /// </summary>
        [JsonPropertyName("total_balance_fiat")]
        public decimal TotalBalanceFiat { get; set; }
        /// <summary>
        /// Total balance crypto
        /// </summary>
        [JsonPropertyName("total_balance_crypto")]
        public decimal TotalBalanceCrypto { get; set; }
        /// <summary>
        /// Available to trade fiat
        /// </summary>
        [JsonPropertyName("available_to_trade_fiat")]
        public decimal AvailableToTradeFiat { get; set; }
        /// <summary>
        /// Allocation
        /// </summary>
        [JsonPropertyName("allocation")]
        public decimal Allocation { get; set; }
        /// <summary>
        /// One day change
        /// </summary>
        [JsonPropertyName("one_day_change")]
        public decimal OneDayChange { get; set; }
        /// <summary>
        /// Cost basis
        /// </summary>
        [JsonPropertyName("cost_basis")]
        public CoinbaseQuantityReference CostBasis { get; set; } = null!;
        /// <summary>
        /// Asset image url
        /// </summary>
        [JsonPropertyName("asset_img_url")]
        public string AssetImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// Is cash
        /// </summary>
        [JsonPropertyName("is_cash")]
        public bool IsCash { get; set; }
    }

    /// <summary>
    /// Position info
    /// </summary>
    public record CoinbasePorfolioBreakdownPosition
    {
        /// <summary>
        /// Product id
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Product uuid
        /// </summary>
        [JsonPropertyName("product_uuid")]
        public string ProductUuid { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset image url
        /// </summary>
        [JsonPropertyName("asset_image_url")]
        public string AssetImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// Vwap
        /// </summary>
        [JsonPropertyName("vwap")]
        public CoinbaseCurrencyQuantityReference Vwap { get; set; } = null!;
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public PositionSide? PositionSide { get; set; }
        /// <summary>
        /// Net quantity
        /// </summary>
        [JsonPropertyName("net_size")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// Buy order quantity
        /// </summary>
        [JsonPropertyName("buy_order_size")]
        public decimal BuyOrderQuantity { get; set; }
        /// <summary>
        /// Sell order quantity
        /// </summary>
        [JsonPropertyName("sell_order_size")]
        public decimal SellOrderQuantity { get; set; }
        /// <summary>
        /// Initial margin contribution
        /// </summary>
        [JsonPropertyName("im_contribution")]
        public decimal InitialMarginContribution { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseCurrencyQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public CoinbaseCurrencyQuantityReference MarkPrice { get; set; } = null!;
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public CoinbaseCurrencyQuantityReference LiquidationPrice { get; set; } = null!;
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Initial margin notional
        /// </summary>
        [JsonPropertyName("im_notional")]
        public CoinbaseCurrencyQuantityReference InitialMarginNotional { get; set; } = null!;
        /// <summary>
        /// Maintenance margin notional
        /// </summary>
        [JsonPropertyName("mm_notional")]
        public CoinbaseCurrencyQuantityReference MaintenanceMarginNotional { get; set; } = null!;
        /// <summary>
        /// Position notional
        /// </summary>
        [JsonPropertyName("position_notional")]
        public CoinbaseCurrencyQuantityReference PositionNotional { get; set; } = null!;
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal LiquidationBuffer { get; set; }
        /// <summary>
        /// Liquidation percentage
        /// </summary>
        [JsonPropertyName("liquidation_percentage")]
        public decimal LiquidationPercentage { get; set; }
    }

    /// <summary>
    /// Futures position info
    /// </summary>
    public record CoinbasePorfolioBreakdownFuturesPosition
    {
        /// <summary>
        /// Product id
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// Contract quantity
        /// </summary>
        [JsonPropertyName("contract_size")]
        public decimal ContractQuantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public PositionSide? Side { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("avg_entry_price")]
        public decimal? AverageEntryPrice { get; set; }
        /// <summary>
        /// Current price
        /// </summary>
        [JsonPropertyName("current_price")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// Unrealized pnl
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Expiry
        /// </summary>
        [JsonPropertyName("expiry")]
        public DateTime Expiry { get; set; }
        /// <summary>
        /// Underlying asset
        /// </summary>
        [JsonPropertyName("underlying_asset")]
        public string UnderlyingAsset { get; set; } = string.Empty;
        /// <summary>
        /// Asset img url
        /// </summary>
        [JsonPropertyName("asset_img_url")]
        public string AssetImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// Product name
        /// </summary>
        [JsonPropertyName("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// Venue
        /// </summary>
        [JsonPropertyName("venue")]
        public string Venue { get; set; } = string.Empty;
        /// <summary>
        /// Notional value
        /// </summary>
        [JsonPropertyName("notional_value")]
        public decimal NotionalValue { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public record CoinbaseCurrencyQuantityReference
    {
        /// <summary>
        /// User native asset
        /// </summary>
        [JsonPropertyName("userNativeCurrency")]
        public CoinbaseQuantityReference UserNativeAsset { get; set; } = null!;
        /// <summary>
        /// Raw asset
        /// </summary>
        [JsonPropertyName("rawCurrency")]
        public CoinbaseQuantityReference RawAsset { get; set; } = null!;
    }
}
