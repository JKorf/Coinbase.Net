using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// Fills for an advanced trade order
        /// </summary>
        [Map("advanced_trade_fill")]
        AdvancedTradeFill,
        /// <summary>
        /// Buy a digital asset
        /// </summary>
        [Map("buy")]
        Buy,
        /// <summary>
        /// Recover money already disbursed
        /// </summary>
        [Map("clawback")]
        Clawback,
        /// <summary>
        /// Daily settlement between spot and futures accounts for US futures product
        /// </summary>
        [Map("derivatives_settlement")]
        DerivativesSettlement,
        /// <summary>
        /// Payout for user earn on Coinbase
        /// </summary>
        [Map("earn_payout")]
        EarnPayout,
        /// <summary>
        /// Deposit funds into a fiat account from a financial institution
        /// </summary>
        [Map("fiat_deposit")]
        FiatDeposit,
        /// <summary>
        /// Withdraw funds from a fiat account
        /// </summary>
        [Map("fiat_withdrawal")]
        FiatWithdrawal,
        /// <summary>
        /// Clawback incentive payout from customer account
        /// </summary>
        [Map("incentives_shared_clawback")]
        IncentivesSharedClawback,
        /// <summary>
        /// Deposit crypto to customer international account
        /// </summary>
        [Map("intx_deposit")]
        IntxDeposit,
        /// <summary>
        /// Withdraw crypto from customer international account
        /// </summary>
        [Map("intx_withdrawal")]
        IntxWithdrawal,
        /// <summary>
        /// Receive a digital asset
        /// </summary>
        [Map("receive")]
        Receive,
        /// <summary>
        /// Request a digital asset from a user or email
        /// </summary>
        [Map("request")]
        Request,
        /// <summary>
        /// Sell a digital asset
        /// </summary>
        [Map("sell")]
        Sell,
        /// <summary>
        /// Send a supported digital asset to a corresponding address or email.
        /// </summary>
        [Map("send")]
        Send,
        /// <summary>
        /// Funds from primary account moved to staked account
        /// </summary>
        [Map("staking_transfer")]
        StakingTransfer,
        /// <summary>
        /// Transaction for Coinbase subscription rebate
        /// </summary>
        [Map("subscription_rebate")]
        SubscriptionRebate,
        /// <summary>
        /// Transaction for Coinbase subscription
        /// </summary>
        [Map("subscription")]
        Subscription,
        /// <summary>
        /// Exchange one cryptocurrency for another cryptocurrency or fiat currency
        /// </summary>
        [Map("trade")]
        Trade,
        /// <summary>
        /// Transfer funds between two of your own accounts
        /// </summary>
        [Map("transfer")]
        Transfer,
        /// <summary>
        /// Default transaction type, uncategorized.
        /// </summary>
        [Map("tx")]
        Transaction,
        /// <summary>
        /// Funds from staked funds moved to primary account
        /// </summary>
        [Map("unstaking_transfer")]
        UnstakingTransfer,
        /// <summary>
        /// Recover unsupported ERC-20s deposited to Coinbase on ethereum mainnet
        /// </summary>
        [Map("unsupported_asset_recovery")]
        UnsupportedAssetRecovery,
        /// <summary>
        /// Unwrap wrapped assets, e.g. cbETH, to wrappable assets, e.g. staked ETH
        /// </summary>
        [Map("unwrap_asset")]
        UnwrapAsset,
        /// <summary>
        /// Withdraw funds from a vault account
        /// </summary>
        [Map("vault_withdrawal")]
        VaultWithdrawal,
        /// <summary>
        /// Wrap wrappable assets, e.g. staked ETH, to wrapped assets, e.g. cbETH
        /// </summary>
        [Map("wrap_asset")]
        WrapAsset,
    }
}
