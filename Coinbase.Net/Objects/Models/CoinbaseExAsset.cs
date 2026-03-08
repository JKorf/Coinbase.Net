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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>display_name</c>"] Display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_size</c>"] Min size
        /// </summary>
        [JsonPropertyName("min_size")]
        public decimal MinSize { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>status_message</c>"] Status info
        /// </summary>
        [JsonPropertyName("status_message")]
        public string? StatusMessage { get; set; }
        /// <summary>
        /// ["<c>max_precision</c>"] Max precision
        /// </summary>
        [JsonPropertyName("max_precision")]
        public decimal MaxPrecision { get; set; }
        /// <summary>
        /// ["<c>convertible_to</c>"] Convertible to
        /// </summary>
        [JsonPropertyName("convertible_to")]
        public string[] ConvertibleTo { get; set; } = [];
        /// <summary>
        /// ["<c>default_network</c>"] Default network
        /// </summary>
        [JsonPropertyName("default_network")]
        public string DefaultNetwork { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>supported_networks</c>"] Supported networks
        /// </summary>
        [JsonPropertyName("supported_networks")]
        public CoinbaseExAssetNetwork[] SupportedNetworks { get; set; } = [];

        /// <summary>
        /// ["<c>details</c>"] Detailed info
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Full name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>contract_address</c>"] Contract address
        /// </summary>
        [JsonPropertyName("contract_address")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>crypto_address_link</c>"] Crypto address link
        /// </summary>
        [JsonPropertyName("crypto_address_link")]
        public string AddressLink { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>crypto_transaction_link</c>"] Crypto transaction link
        /// </summary>
        [JsonPropertyName("crypto_transaction_link")]
        public string TransactionLink { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_withdrawal_amount</c>"] Min withdraw quantity
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>max_withdrawal_amount</c>"] Max withdraw quantity
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// ["<c>network_confirmations</c>"] Network confirmations required
        /// </summary>
        [JsonPropertyName("network_confirmations")]
        public int? NetworkConfirmations { get; set; }
        /// <summary>
        /// ["<c>processing_time_seconds</c>"] Processing time in seconds
        /// </summary>
        [JsonPropertyName("processing_time_seconds")]
        public int? ProcessingTime { get; set; }
        /// <summary>
        /// ["<c>destination_tag_regex</c>"] Destination tag regex
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
        /// ["<c>type</c>"] Asset type
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType Type { get; set; }

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>network_confirmations</c>"] The number of network confirmations associated with the transaction.
        /// </summary>
        [JsonPropertyName("network_confirmations")]
        public int? NetworkConfirmations { get; set; }

        /// <summary>
        /// ["<c>sort_order</c>"] The position of the item in a sorted sequence.
        /// </summary>
        [JsonPropertyName("sort_order")]
        public int? SortOrder { get; set; }

        /// <summary>
        /// ["<c>crypto_address_link</c>"] The link template for viewing crypto addresses.
        /// </summary>
        [JsonPropertyName("crypto_address_link")]
        public string CryptoAddressLink { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>crypto_transaction_link</c>"] The URL or reference link to the associated cryptocurrency transaction.
        /// </summary>
        [JsonPropertyName("crypto_transaction_link")]
        public string CryptoTransactionLink { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>push_payment_methods</c>"] The list of supported push payment methods for the transaction.
        /// </summary>
        [JsonPropertyName("push_payment_methods")]
        public List<string> PushPaymentMethods { get; set; } = null!;

        /// <summary>
        /// ["<c>group_types</c>"] The collection of group type identifiers associated with the entity.
        /// </summary>
        [JsonPropertyName("group_types")]
        public List<string> GroupTypes { get; set; } = null!;

        /// <summary>
        /// ["<c>display_name</c>"] The display name
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>processing_time_seconds</c>"] The estimated processing time in seconds for transactions involving this asset.
        /// </summary>
        [JsonPropertyName("processing_time_seconds")]
        public int? ProcessingTimeSeconds { get; set; }

        /// <summary>
        /// ["<c>min_withdrawal_amount</c>"] The minimum amount that can be withdrawn for this asset.
        /// </summary>
        [JsonPropertyName("min_withdrawal_amount")]
        public decimal? MinWithdrawalAmount { get; set; }

        /// <summary>
        /// ["<c>max_withdrawal_amount</c>"] The maximum amount that can be withdrawn for this asset.
        /// </summary>
        [JsonPropertyName("max_withdrawal_amount")]
        public decimal? MaxWithdrawalAmount { get; set; }
    }
}
