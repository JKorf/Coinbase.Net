using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Portfolios info
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPorfolios
    {
        /// <summary>
        /// Portfolios
        /// </summary>
        [JsonPropertyName("portfolios")]
        public CoinbasePerpetualPorfolio[] Portfolios { get; set; } = Array.Empty<CoinbasePerpetualPorfolio>();
        /// <summary>
        /// Summary
        /// </summary>
        [JsonPropertyName("summary")]
        public CoinbasePerpetualPorfolioSummary Summary { get; set; } = null!;
    }

    /// <summary>
    /// Portfolio info
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPorfolio
    {
        /// <summary>
        /// Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// Collateral
        /// </summary>
        [JsonPropertyName("collateral")]
        public decimal Collateral { get; set; }
        /// <summary>
        /// Position notional
        /// </summary>
        [JsonPropertyName("position_notional")]
        public decimal PositionNotional { get; set; }
        /// <summary>
        /// Open position notional
        /// </summary>
        [JsonPropertyName("open_position_notional")]
        public decimal OpenPositionNotional { get; set; }
        /// <summary>
        /// Pending fees
        /// </summary>
        [JsonPropertyName("pending_fees")]
        public decimal PendingFees { get; set; }
        /// <summary>
        /// Borrow
        /// </summary>
        [JsonPropertyName("borrow")]
        public decimal Borrow { get; set; }
        /// <summary>
        /// Accrued interest
        /// </summary>
        [JsonPropertyName("accrued_interest")]
        public decimal AccruedInterest { get; set; }
        /// <summary>
        /// Rolling debt
        /// </summary>
        [JsonPropertyName("rolling_debt")]
        public decimal RollingDebt { get; set; }
        /// <summary>
        /// Portfolio initial margin
        /// </summary>
        [JsonPropertyName("portfolio_initial_margin")]
        public CoinbaseQuantityReference PortfolioInitialMargin { get; set; } = null!;
        /// <summary>
        /// Portfolio im notional
        /// </summary>
        [JsonPropertyName("portfolio_im_notional")]
        public decimal PortfolioImNotional { get; set; }
        /// <summary>
        /// Portfolio maintenance margin
        /// </summary>
        [JsonPropertyName("portfolio_maintenance_margin")]
        public decimal PortfolioMaintenanceMargin { get; set; }
        /// <summary>
        /// Portfolio mm notional
        /// </summary>
        [JsonPropertyName("portfolio_mm_notional")]
        public CoinbaseQuantityReference PortfolioMmNotional { get; set; } = null!;
        /// <summary>
        /// Liquidation percentage
        /// </summary>
        [JsonPropertyName("liquidation_percentage")]
        public decimal LiquidationPercentage { get; set; }
        /// <summary>
        /// Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal LiquidationBuffer { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Margin flags
        /// </summary>
        [JsonPropertyName("margin_flags")]
        public MarginFlags MarginFlags { get; set; }
        /// <summary>
        /// Liquidation status
        /// </summary>
        [JsonPropertyName("liquidation_status")]
        public LiquidationStatus LiquidationStatus { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public CoinbaseQuantityReference TotalBalance { get; set; } = null!;
    }

    /// <summary>
    /// Perpetual futures portfolio summary
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPorfolioSummary
    {
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// Buying power
        /// </summary>
        [JsonPropertyName("buying_power")]
        public CoinbaseQuantityReference BuyingPower { get; set; } = null!;
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public CoinbaseQuantityReference TotalBalance { get; set; } = null!;
        /// <summary>
        /// Max withdrawal quantity
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public CoinbaseQuantityReference MaxWithdrawalQuantity { get; set; } = null!;
    }
}
