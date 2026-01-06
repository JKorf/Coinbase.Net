using Coinbase.Net.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Asset info
    /// </summary>
    public record CoinbaseExAsset
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Min size
        /// </summary>
        [JsonPropertyName("min_size")]
        public decimal MinSize { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Status info
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// Max precision
        /// </summary>
        [JsonPropertyName("max_precision")]
        public decimal MaxPrecision { get; set; }
        /// <summary>
        /// Convertible to
        /// </summary>
        [JsonPropertyName("convertible_to")]
        public string[] ConvertibleTo { get; set; } = [];
        /// <summary>
        /// Default network
        /// </summary>
        [JsonPropertyName("default_network")]
        public string DefaultNetwork { get; set; } = string.Empty;
        /// <summary>
        /// Supported networks
        /// </summary>
        [JsonPropertyName("supported_networks")]
        public CoinbaseExAssetNetwork[] SupportedNetworks { get; set; } = [];

        /// <summary>
        /// Detailed info
        /// </summary>
        [JsonPropertyName("details")]
        public CoinbaseExAssetDetails? Details { get; set; }
    }

    /// <summary>
    /// Asset network
    /// </summary>
    public record CoinbaseExAssetNetwork
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Crypto address link
        /// </summary>
        [JsonPropertyName("crypto_address_link")]
        public string AddressLink { get; set; } = string.Empty;
        /// <summary>
        /// Crypto transaction link
        /// </summary>
        [JsonPropertyName("crypto_transaction_link")]
        public string TransactionLink { get; set; } = string.Empty;
        /// <summary>
        /// Min withdraw quantity
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Max withdraw quantity
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Network confirmations required
        /// </summary>
        [JsonPropertyName("network_confirmations")]
        public int? NetworkConfirmations { get; set; }
        /// <summary>
        /// Processing time in seconds
        /// </summary>
        [JsonPropertyName("processing_time_seconds")]
        public int? ProcessingTime { get; set; }
        /// <summary>
        /// Destination tag regex
        /// </summary>
        [JsonPropertyName("destination_tag_regex")]
        public string DestinationTagRegex { get; set; } = string.Empty;
    }

    /// <summary>
    /// Detailed asset info
    /// </summary>
    public class CoinbaseExAssetDetails
    {
        /// <summary>
        /// Asset type
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType Type { get; set; }

        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The number of network confirmations associated with the transaction.
        /// </summary>
        [JsonPropertyName("network_confirmations")]
        public int? NetworkConfirmations { get; set; }

        /// <summary>
        /// The position of the item in a sorted sequence.
        /// </summary>
        [JsonPropertyName("sort_order")]
        public int? SortOrder { get; set; }

        /// <summary>
        /// The link template for viewing crypto addresses.
        /// </summary>
        [JsonPropertyName("crypto_address_link")]
        public string CryptoAddressLink { get; set; } = string.Empty;

        /// <summary>
        /// The URL or reference link to the associated cryptocurrency transaction.
        /// </summary>
        [JsonPropertyName("crypto_transaction_link")]
        public string CryptoTransactionLink { get; set; } = string.Empty;

        /// <summary>
        /// The list of supported push payment methods for the transaction.
        /// </summary>
        [JsonPropertyName("push_payment_methods")]
        public List<string> PushPaymentMethods { get; set; } = null!;

        /// <summary>
        /// The collection of group type identifiers associated with the entity.
        /// </summary>
        [JsonPropertyName("group_types")]
        public List<string> GroupTypes { get; set; } = null!;

        /// <summary>
        /// The display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// The estimated processing time in seconds for transactions involving this asset.
        /// </summary>
        [JsonPropertyName("processing_time_seconds")]
        public int? ProcessingTimeSeconds { get; set; }

        /// <summary>
        /// The minimum amount that can be withdrawn for this asset.
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal? MinWithdrawalAmount { get; set; }

        /// <summary>
        /// The maximum amount that can be withdrawn for this asset.
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public decimal? MaxWithdrawalAmount { get; set; }
    }
}
