using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePaymentMethodsWrapper
    {
        /// <summary>
        /// ["<c>payment_methods</c>"] Payment methods
        /// </summary>
        [JsonPropertyName("payment_methods")]
        public CoinbasePaymentMethod[] PaymentMethods { get; set; } = null!;
    }

    [SerializationModel]
    internal record CoinbasePaymentMethodWrapper
    {
        /// <summary>
        /// ["<c>payment_method</c>"] Payment methods
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CoinbasePaymentMethod PaymentMethod { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbasePaymentMethod
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>verified</c>"] Verified
        /// </summary>
        [JsonPropertyName("verified")]
        public bool Verified { get; set; }
        /// <summary>
        /// ["<c>allow_buy</c>"] Allow buy
        /// </summary>
        [JsonPropertyName("allow_buy")]
        public bool AllowBuy { get; set; }
        /// <summary>
        /// ["<c>allow_sell</c>"] Allow sell
        /// </summary>
        [JsonPropertyName("allow_sell")]
        public bool AllowSell { get; set; }
        /// <summary>
        /// ["<c>allow_deposit</c>"] Allow deposit
        /// </summary>
        [JsonPropertyName("allow_deposit")]
        public bool AllowDeposit { get; set; }
        /// <summary>
        /// ["<c>allow_withdraw</c>"] Allow withdraw
        /// </summary>
        [JsonPropertyName("allow_withdraw")]
        public bool AllowWithdraw { get; set; }
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
    }


}
