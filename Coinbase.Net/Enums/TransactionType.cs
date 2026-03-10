using System.Text.Json.Serialization;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// ["<c>advanced_trade_fill</c>"] Fills for an advanced trade order
        /// </summary>
        [Map("advanced_trade_fill")]
        AdvancedTradeFill,
        /// <summary>
        /// ["<c>buy</c>"] Buy a digital asset
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// ["<c>clawback</c>"] Recover money already disbursed
        /// </summary>
        [Map("clawback")]
        Clawback,
        /// <summary>
        /// ["<c>derivatives_settlement</c>"] Daily settlement between spot and futures accounts for US futures product
        /// </summary>
        [Map("derivatives_settlement")]
        DerivativesSettlement,
        /// <summary>
        /// ["<c>earn_payout</c>"] Payout for user earn on Coinbase
        /// </summary>
        [Map("earn_payout")]
        EarnPayout,
        /// <summary>
        /// ["<c>fiat_deposit</c>"] Deposit funds into a fiat account from a financial institution
        /// </summary>
        [Map("fiat_deposit")]
        FiatDeposit,
        /// <summary>
        /// ["<c>fiat_withdrawal</c>"] Withdraw funds from a fiat account
        /// </summary>
        [Map("fiat_withdrawal")]
        FiatWithdrawal,
        /// <summary>
        /// ["<c>incentives_shared_clawback</c>"] Clawback incentive payout from customer account
        /// </summary>
        [Map("incentives_shared_clawback")]
        IncentivesSharedClawback,
        /// <summary>
        /// ["<c>intx_deposit</c>"] Deposit crypto to customer international account
        /// </summary>
        [Map("intx_deposit")]
        IntxDeposit,
        /// <summary>
        /// ["<c>intx_withdrawal</c>"] Withdraw crypto from customer international account
        /// </summary>
        [Map("intx_withdrawal")]
        IntxWithdrawal,
        /// <summary>
        /// ["<c>receive</c>"] Receive a digital asset
        /// </summary>
        [Map("receive")]
        Receive,
        /// <summary>
        /// ["<c>request</c>"] Request a digital asset from a user or email
        /// </summary>
        [Map("request")]
        Request,
        /// <summary>
        /// ["<c>sell</c>"] Sell a digital asset
        /// </summary>
        [Map("sell")]
        Sell,
        /// <summary>
        /// ["<c>send</c>"] Send a supported digital asset to a corresponding address or email.
        /// </summary>
        [Map("send")]
        Send,
        /// <summary>
        /// ["<c>staking_transfer</c>"] Funds from primary account moved to staked account
        /// </summary>
        [Map("staking_transfer")]
        StakingTransfer,
        /// <summary>
        /// ["<c>subscription_rebate</c>"] Transaction for Coinbase subscription rebate
        /// </summary>
        [Map("subscription_rebate")]
        SubscriptionRebate,
        /// <summary>
        /// ["<c>subscription</c>"] Transaction for Coinbase subscription
        /// </summary>
        [Map("subscription")]
        Subscription,
        /// <summary>
        /// ["<c>trade</c>"] Exchange one cryptocurrency for another cryptocurrency or fiat currency
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// ["<c>transfer</c>"] Transfer funds between two of your own accounts
        /// </summary>
        [Map("transfer")]
        Transfer,
        /// <summary>
        /// ["<c>tx</c>"] Default transaction type, uncategorized.
        /// </summary>
        [Map("tx")]
        Transaction,
        /// <summary>
        /// ["<c>unstaking_transfer</c>"] Funds from staked funds moved to primary account
        /// </summary>
        [Map("unstaking_transfer")]
        UnstakingTransfer,
        /// <summary>
        /// ["<c>unsupported_asset_recovery</c>"] Recover unsupported ERC-20s deposited to Coinbase on ethereum mainnet
        /// </summary>
        [Map("unsupported_asset_recovery")]
        UnsupportedAssetRecovery,
        /// <summary>
        /// ["<c>unwrap_asset</c>"] Unwrap wrapped assets, e.g. cbETH, to wrappable assets, e.g. staked ETH
        /// </summary>
        [Map("unwrap_asset")]
        UnwrapAsset,
        /// <summary>
        /// ["<c>vault_withdrawal</c>"] Withdraw funds from a vault account
        /// </summary>
        [Map("vault_withdrawal")]
        VaultWithdrawal,
        /// <summary>
        /// ["<c>wrap_asset</c>"] Wrap wrappable assets, e.g. staked ETH, to wrapped assets, e.g. cbETH
        /// </summary>
        [Map("wrap_asset")]
        WrapAsset,
        /// <summary>
        /// ["<c>pro_withdrawal</c>"] Withdrawal from Coinbase Pro
        /// </summary>
        [Map("pro_withdrawal")]
        ProWithdrawal,
        /// <summary>
        /// ["<c>pro_deposit</c>"] Deposit to Coinbase Pro
        /// </summary>
        [Map("pro_deposit")]
        ProDeposit,
        /// <summary>
        /// ["<c>interest</c>"] Interest payout
        /// </summary>
        [Map("interest")]
        Interest,
        /// <summary>
        /// ["<c>asset_migration</c>"] Asset migration
        /// </summary>
        [Map("asset_migration")]
        AssetMigration
    }
}
