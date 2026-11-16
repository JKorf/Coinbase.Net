# Operational considerations

## Credentials and account scope

Coinbase credentials are an API key name and EC private key, without a HMAC secret or passphrase. Preserve PEM line breaks, keep key material outside source, and grant the permissions needed for the selected operations. Authentication in the `netstandard2.0` library build throws `PlatformNotSupportedException`; select a compatible asset for private requests.

Account UUIDs and portfolio IDs identify different scopes. Verify the account, portfolio, asset, and available balance for account-changing operations. Coinbase App deposits/withdrawals use account-specific routes under `AdvancedTradeApi.Account`.

## Order and cancellation outcomes

Native placement returns `CoinbaseOrderResult` with a nested `Success` flag. The library maps reported rejections to outer errors in many cases, but check both success layers before reading `SuccessResponse.OrderId`; use `ErrorResponse` when the nested result rejects the order.

`CancelOrdersAsync` returns individual `CoinbaseCancelResult` entries. After the outer request succeeds, inspect each item's `Success` and `ErrorMessage`. Single-order cancellation also checks its item result. Cancellation can race with a fill; reconcile executions and order state before assuming an order had no fills.

Limit orders can execute immediately when their price crosses the book. Select base `quantity` versus `quoteQuantity` deliberately for market orders. Use symbol price/quantity increments and minimums instead of hardcoded precision.

## Submission recovery

The library generates a client-order ID when omitted. Persist an explicit ID when the workflow needs durable correlation across an uncertain submission. A timeout can occur after acceptance; reconcile orders, fills, or transactions before deciding to resubmit. A transient transport error does not make a repeated account-changing call safe. Handle bounded public-read retries separately from submission recovery.

## Derivatives and fund movements

Use current futures/perpetual product metadata and account eligibility. Contract quantities differ from spot base quantities. Perpetual balances/positions require the correct portfolio; collateral mode, allocation, and close-position operations can change exposure. Shared non-market reduce-only futures requests are rejected; the market reduce-only path closes a position.

Portfolio transfers/allocation, fiat deposits/withdrawals, crypto withdrawal, convert commitment, and address creation change funds or account state. Preserve account direction, payment method, asset, destination, network, and required memo/tag where the endpoint exposes them. Request acceptance is distinct from completed settlement.

The complete trading example gates live placement with `COINBASE_EXAMPLE_PLACE_ORDER=true`; that is sample configuration, not an exchange test-order endpoint.
