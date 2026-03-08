using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
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
        /// ["<c>portfolios</c>"] Portfolios
        /// </summary>
        [JsonPropertyName("portfolios")]
        public CoinbasePerpetualPorfolio[] Portfolios { get; set; } = Array.Empty<CoinbasePerpetualPorfolio>();
        /// <summary>
        /// ["<c>summary</c>"] Summary
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
        /// ["<c>portfolio_uuid</c>"] Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateral</c>"] Collateral
        /// </summary>
        [JsonPropertyName("collateral")]
        public decimal Collateral { get; set; }
        /// <summary>
        /// ["<c>position_notional</c>"] Position notional
        /// </summary>
        [JsonPropertyName("position_notional")]
        public decimal PositionNotional { get; set; }
        /// <summary>
        /// ["<c>open_position_notional</c>"] Open position notional
        /// </summary>
        [JsonPropertyName("open_position_notional")]
        public decimal OpenPositionNotional { get; set; }
        /// <summary>
        /// ["<c>pending_fees</c>"] Pending fees
        /// </summary>
        [JsonPropertyName("pending_fees")]
        public decimal PendingFees { get; set; }
        /// <summary>
        /// ["<c>borrow</c>"] Borrow
        /// </summary>
        [JsonPropertyName("borrow")]
        public decimal Borrow { get; set; }
        /// <summary>
        /// ["<c>accrued_interest</c>"] Accrued interest
        /// </summary>
        [JsonPropertyName("accrued_interest")]
        public decimal AccruedInterest { get; set; }
        /// <summary>
        /// ["<c>rolling_debt</c>"] Rolling debt
        /// </summary>
        [JsonPropertyName("rolling_debt")]
        public decimal RollingDebt { get; set; }
        /// <summary>
        /// ["<c>portfolio_initial_margin</c>"] Portfolio initial margin
        /// </summary>
        [JsonPropertyName("portfolio_initial_margin")]
        public CoinbaseQuantityReference PortfolioInitialMargin { get; set; } = null!;
        /// <summary>
        /// ["<c>portfolio_im_notional</c>"] Portfolio im notional
        /// </summary>
        [JsonPropertyName("portfolio_im_notional")]
        public decimal PortfolioImNotional { get; set; }
        /// <summary>
        /// ["<c>portfolio_maintenance_margin</c>"] Portfolio maintenance margin
        /// </summary>
        [JsonPropertyName("portfolio_maintenance_margin")]
        public decimal PortfolioMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>portfolio_mm_notional</c>"] Portfolio mm notional
        /// </summary>
        [JsonPropertyName("portfolio_mm_notional")]
        public CoinbaseQuantityReference PortfolioMmNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_percentage</c>"] Liquidation percentage
        /// </summary>
        [JsonPropertyName("liquidation_percentage")]
        public decimal LiquidationPercentage { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer</c>"] Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal LiquidationBuffer { get; set; }
        /// <summary>
        /// ["<c>margin_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>margin_flags</c>"] Margin flags
        /// </summary>
        [JsonPropertyName("margin_flags")]
        public MarginFlags MarginFlags { get; set; }
        /// <summary>
        /// ["<c>liquidation_status</c>"] Liquidation status
        /// </summary>
        [JsonPropertyName("liquidation_status")]
        public LiquidationStatus LiquidationStatus { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>total_balance</c>"] Total balance
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
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>buying_power</c>"] Buying power
        /// </summary>
        [JsonPropertyName("buying_power")]
        public CoinbaseQuantityReference BuyingPower { get; set; } = null!;
        /// <summary>
        /// ["<c>total_balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public CoinbaseQuantityReference TotalBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>max_withdrawal_amount</c>"] Max withdrawal quantity
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public CoinbaseQuantityReference MaxWithdrawalQuantity { get; set; } = null!;
    }
}
