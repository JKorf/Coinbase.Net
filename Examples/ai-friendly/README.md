# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable**: drop into a console project with `dotnet add package Coinbase.Net` and it builds.
- **Self-contained**: single file, no shared helpers.
- **Heavily commented**: explains why each line matters for Coinbase.Net.
- **Coinbase-shaped**: uses `AdvancedTradeApi`, `ExchangeApi`, ECDSA credentials, and dash-separated product ids.

## Files

| File | What it shows |
|---|---|
| `01-advanced-trade-market-and-account.cs` | Advanced Trade public product, candles, order book, Exchange API products, and credential-gated accounts |
| `02-advanced-trade-trading.cs` | Dry-run safe order workflow, open orders, fills, order placement, and cancellation |
| `03-websocket.cs` | Advanced Trade ticker/order book streams, optional authenticated user stream, and teardown |
| `04-shared-client.cs` | CryptoExchange.Net SharedApis access, capability discovery, and native Coinbase endpoints |
| `05-error-handling.cs` | `HttpResult` / `WebSocketResult` helpers and `CoinbaseOrderResult` handling |

## Running

```bash
dotnet new console -n MyCoinbaseApp
cd MyCoinbaseApp
dotnet add package Coinbase.Net
# Copy one example file into Program.cs
dotnet run
```

Private examples read credentials from:

- `COINBASE_API_KEY_NAME`
- `COINBASE_API_PRIVATE_KEY`

The private key can contain real newlines or escaped `\n` sequences. The examples normalize escaped newlines.

The trading example defaults to dry-run mode. Set `COINBASE_EXAMPLE_PLACE_ORDER=true` only after reviewing product id, quantity, price, account permissions, and private key configuration.
