# Coinbase.Net examples

Install `JKorf.Coinbase.Net`. See [usage](../../.claude/skills/coinbase-net/references/usage.md), [API routing](../../.claude/skills/coinbase-net/references/api-surfaces.md), and [operational considerations](../../.claude/skills/coinbase-net/references/safety.md) for maintained consumer guidance.

- [Advanced Trade market and account](01-advanced-trade-market-and-account.cs)
- [Advanced Trade trading](02-advanced-trade-trading.cs)
- [WebSockets](03-websocket.cs)
- [Shared API V2](04-shared-client.cs)
- [Error handling](05-error-handling.cs)

The documentation test compiles these files as independent console programs without executing exchange operations. Running them contacts Coinbase. The trading example gates live placement with `COINBASE_EXAMPLE_PLACE_ORDER=true`; review its parameters before enabling it.

See [repository instructions](../../AGENTS.md) for build and test commands.
