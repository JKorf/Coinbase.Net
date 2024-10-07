using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Liquidation status
    /// </summary>
    public enum LiquidationStatus
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Canceling
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_CANCELING")]
        Canceling,
        /// <summary>
        /// Auto liquidating
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_AUTO_LIQUIDATING")]
        AutoLiquidating,
        /// <summary>
        /// LSP assignment
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_LSP_ASSIGNMENT")]
        LspAssignment,
        /// <summary>
        /// Customer assignment
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_CUSTOMER_ASSIGNMENT")]
        CustomerAssignment,
        /// <summary>
        /// Manual liquidation
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_MANUAL")]
        Manual,
        /// <summary>
        /// Not liquidating
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_NOT_LIQUIDATING")]
        NotLiquidating,
    }
}
