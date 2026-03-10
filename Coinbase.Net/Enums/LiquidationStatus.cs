using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Liquidation status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LiquidationStatus>))]
    public enum LiquidationStatus
    {
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_CANCELING</c>"] Canceling
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_CANCELING")]
        Canceling,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_AUTO_LIQUIDATING</c>"] Auto liquidating
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_AUTO_LIQUIDATING")]
        AutoLiquidating,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_LSP_ASSIGNMENT</c>"] LSP assignment
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_LSP_ASSIGNMENT")]
        LspAssignment,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_CUSTOMER_ASSIGNMENT</c>"] Customer assignment
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_CUSTOMER_ASSIGNMENT")]
        CustomerAssignment,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_MANUAL</c>"] Manual liquidation
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_MANUAL")]
        Manual,
        /// <summary>
        /// ["<c>PORTFOLIO_LIQUIDATION_STATUS_NOT_LIQUIDATING</c>"] Not liquidating
        /// </summary>
        [Map("PORTFOLIO_LIQUIDATION_STATUS_NOT_LIQUIDATING")]
        NotLiquidating,
    }
}
