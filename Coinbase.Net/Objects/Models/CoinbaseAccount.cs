using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseAccountWrapper
    {
        [JsonPropertyName("account")]
        public CoinbaseAccount Account { get; set; } = null!;
    }

    /// <summary>
    /// Accounts page
    /// </summary>
    [SerializationModel]
    public record CoinbaseAccountPage : CoinbasePage
    {
        /// <summary>
        /// ["<c>accounts</c>"] Accounts
        /// </summary>
        [JsonPropertyName("accounts")]
        public CoinbaseAccount[] Accounts { get; set; } = Array.Empty<CoinbaseAccount>();
    }

    /// <summary>
    /// Account/balance info
    /// </summary>
    [SerializationModel]
    public record CoinbaseAccount
    {
        /// <summary>
        /// ["<c>uuid</c>"] Account id
        /// </summary>
        [JsonPropertyName("uuid")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Account name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>available_balance</c>"] Available balance
        /// </summary>
        [JsonPropertyName("available_balance")]
        public CoinbaseQuantityReference AvailableBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>hold</c>"] Holding/frozen balance
        /// </summary>
        [JsonPropertyName("hold")]
        public CoinbaseQuantityReference HoldBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>type</c>"] Type of account
        /// </summary>
        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// ["<c>default</c>"] Default
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }
        /// <summary>
        /// ["<c>active</c>"] Active
        /// </summary>
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        /// <summary>
        /// ["<c>ready</c>"] Ready
        /// </summary>
        [JsonPropertyName("ready")]
        public bool Ready { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Create time
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Update time
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>deleted_at</c>"] Delete time
        /// </summary>
        [JsonPropertyName("deleted_at")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// ["<c>retail_portfolio_id</c>"] Portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string PortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>platform</c>"] Account platform
        /// </summary>
        [JsonPropertyName("platform")]
        public AccountPlatform Platform { get; set; }
    }

    /// <summary>
    /// Quantity
    /// </summary>
    [SerializationModel]
    public record CoinbaseQuantityReference
    {
        /// <summary>
        /// ["<c>value</c>"] Value
        /// </summary>
        [JsonPropertyName("value")]
        public decimal Value { get; set; }
        [JsonPropertyName("amount")]
        [JsonInclude]
        internal decimal Amount { set => Value = value; }
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
