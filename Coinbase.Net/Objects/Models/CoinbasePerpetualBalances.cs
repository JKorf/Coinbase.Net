using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePerpetualBalancesWrapper
    {
        /// <summary>
        /// ["<c>portfolio_balances</c>"] Portfolio balances
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
        /// ["<c>portfolio_uuid</c>"] Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>balances</c>"] Balances
        /// </summary>
        [JsonPropertyName("balances")]
        public CoinbasePerpetualBalance[] Balances { get; set; } = Array.Empty<CoinbasePerpetualBalance>();
        /// <summary>
        /// ["<c>is_margin_limit_reached</c>"] Is margin limit reached
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
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public CoinbasePerpetualBalancesAsset Asset { get; set; } = null!;
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>hold</c>"] Hold
        /// </summary>
        [JsonPropertyName("hold")]
        public decimal Hold { get; set; }
        /// <summary>
        /// ["<c>transfer_hold</c>"] Transfer hold
        /// </summary>
        [JsonPropertyName("transfer_hold")]
        public decimal TransferHold { get; set; }
        /// <summary>
        /// ["<c>collateral_value</c>"] Collateral value
        /// </summary>
        [JsonPropertyName("collateral_value")]
        public decimal CollateralValue { get; set; }
        /// <summary>
        /// ["<c>collateral_weight</c>"] Collateral weight
        /// </summary>
        [JsonPropertyName("collateral_weight")]
        public decimal CollateralWeight { get; set; }
        /// <summary>
        /// ["<c>max_withdraw_amount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("max_withdraw_amount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>loan</c>"] Loan
        /// </summary>
        [JsonPropertyName("loan")]
        public decimal Loan { get; set; }
        /// <summary>
        /// ["<c>loan_collateral_requirement_usd</c>"] Loan collateral requirement usd
        /// </summary>
        [JsonPropertyName("loan_collateral_requirement_usd")]
        public decimal LoanCollateralRequirementUsd { get; set; }
        /// <summary>
        /// ["<c>pledged_quantity</c>"] Pledged quantity
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
        /// ["<c>asset_id</c>"] Asset id
        /// </summary>
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset_uuid</c>"] Asset uuid
        /// </summary>
        [JsonPropertyName("asset_uuid")]
        public string AssetUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("asset_name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>collateral_weight</c>"] Collateral weight
        /// </summary>
        [JsonPropertyName("collateral_weight")]
        public decimal CollateralWeight { get; set; }
        /// <summary>
        /// ["<c>account_collateral_limit</c>"] Account collateral limit
        /// </summary>
        [JsonPropertyName("account_collateral_limit")]
        public decimal AccountCollateralLimit { get; set; }
        /// <summary>
        /// ["<c>ecosystem_collateral_limit_breached</c>"] Ecosystem collateral limit breached
        /// </summary>
        [JsonPropertyName("ecosystem_collateral_limit_breached")]
        public bool EcosystemCollateralLimitBreached { get; set; }
        /// <summary>
        /// ["<c>asset_icon_url</c>"] Asset icon url
        /// </summary>
        [JsonPropertyName("asset_icon_url")]
        public string AssetIconUrl { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>supported_networks_enabled</c>"] Supported networks enabled
        /// </summary>
        [JsonPropertyName("supported_networks_enabled")]
        public bool SupportedNetworksEnabled { get; set; }
    }


}
