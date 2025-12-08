using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePerpetualBalancesWrapper
    {
        /// <summary>
        /// Portfolio balances
        /// </summary>
        [JsonPropertyName("portfolio_balances")]
        public CoinbasePerpetualBalances PortfolioBalances { get; set; } = null!;
    }

    /// <summary>
    /// Portfolio balances
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualBalances
    {
        /// <summary>
        /// Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public CoinbasePerpetualBalance[] Balances { get; set; } = Array.Empty<CoinbasePerpetualBalance>();
        /// <summary>
        /// Is margin limit reached
        /// </summary>
        [JsonPropertyName("is_margin_limit_reached")]
        public bool IsMarginLimitReached { get; set; }
    }

    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualBalance
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public CoinbasePerpetualBalancesAsset Asset { get; set; } = null!;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// Transfer hold
        /// </summary>
        [JsonPropertyName("transfer_hold")]
        public decimal TransferHold { get; set; }
        /// <summary>
        /// Collateral value
        /// </summary>
        [JsonPropertyName("collateral_value")]
        public decimal CollateralValue { get; set; }
        /// <summary>
        /// Collateral weight
        /// </summary>
        [JsonPropertyName("collateral_weight")]
        public decimal CollateralWeight { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("max_withdraw_amount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Loan
        /// </summary>
        [JsonPropertyName("loan")]
        public decimal Loan { get; set; }
        /// <summary>
        /// Loan collateral requirement usd
        /// </summary>
        [JsonPropertyName("loan_collateral_requirement_usd")]
        public decimal LoanCollateralRequirementUsd { get; set; }
        /// <summary>
        /// Pledged quantity
        /// </summary>
        [JsonPropertyName("pledged_quantity")]
        public decimal PledgedQuantity { get; set; }
    }

    /// <summary>
    /// Asset
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualBalancesAsset
    {
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// Asset uuid
        /// </summary>
        [JsonPropertyName("asset_uuid")]
        public string AssetUuid { get; set; } = string.Empty;
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("asset_name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Collateral weight
        /// </summary>
        [JsonPropertyName("collateral_weight")]
        public decimal CollateralWeight { get; set; }
        /// <summary>
        /// Account collateral limit
        /// </summary>
        [JsonPropertyName("account_collateral_limit")]
        public decimal AccountCollateralLimit { get; set; }
        /// <summary>
        /// Ecosystem collateral limit breached
        /// </summary>
        [JsonPropertyName("ecosystem_collateral_limit_breached")]
        public bool EcosystemCollateralLimitBreached { get; set; }
        /// <summary>
        /// Asset icon url
        /// </summary>
        [JsonPropertyName("asset_icon_url")]
        public string AssetIconUrl { get; set; } = string.Empty;
        /// <summary>
        /// Supported networks enabled
        /// </summary>
        [JsonPropertyName("supported_networks_enabled")]
        public bool SupportedNetworksEnabled { get; set; }
    }


}
