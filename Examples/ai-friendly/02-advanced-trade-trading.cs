// 02-advanced-trade-trading.cs
//
// Demonstrates: Coinbase Advanced Trade order queries, fills, dry-run safety,
// order placement, and cancellation.
//
// Setup: dotnet add package Coinbase.Net

using Coinbase.Net;
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;

var keyName = Environment.GetEnvironmentVariable("COINBASE_API_KEY_NAME");
var privateKey = Environment.GetEnvironmentVariable("COINBASE_API_PRIVATE_KEY")?.Replace("\\n", "\n");
if (string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(privateKey))
{
    Console.WriteLine("Set COINBASE_API_KEY_NAME and COINBASE_API_PRIVATE_KEY to run authenticated trading calls.");
    return;
}

var restClient = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials(keyName, privateKey);
});

var symbol = "ETH-USD";

var openOrders = await restClient.AdvancedTradeApi.Trading.GetOrdersAsync(
    symbols: new[] { symbol },
    orderStatus: new[] { OrderStatus.Open },
    limit: 20);

if (openOrders.Success)
    Console.WriteLine($"Open orders returned: {openOrders.Data.Length}");
else
    Console.WriteLine($"Open-order request failed: {openOrders.Error}");

var fills = await restClient.AdvancedTradeApi.Trading.GetUserTradesAsync(
    symbols: new[] { symbol },
    limit: 20);

if (fills.Success)
    Console.WriteLine($"Fill rows returned: {fills.Data.Trades.Length}");
else
    Console.WriteLine($"Fill request failed: {fills.Error}");

var placeLiveOrder = string.Equals(
    Environment.GetEnvironmentVariable("COINBASE_EXAMPLE_PLACE_ORDER"),
    "true",
    StringComparison.OrdinalIgnoreCase);

if (!placeLiveOrder)
{
    Console.WriteLine("Dry run only. Set COINBASE_EXAMPLE_PLACE_ORDER=true to place the sample order.");
    return;
}

var order = await restClient.AdvancedTradeApi.Trading.PlaceOrderAsync(
    symbol,
    OrderSide.Buy,
    NewOrderType.Limit,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success)
{
    Console.WriteLine($"Order request failed: {order.Error}");
    return;
}

if (!order.Data.Success)
{
    Console.WriteLine($"Order rejected: {order.Data.ErrorResponse.Message}");
    return;
}

var orderId = order.Data.SuccessResponse.OrderId;
Console.WriteLine($"Placed order: {orderId}");

var cancel = await restClient.AdvancedTradeApi.Trading.CancelOrderAsync(orderId);
Console.WriteLine(cancel.Success
    ? $"Cancel success={cancel.Data.Success}, order={cancel.Data.OrderId}, error={cancel.Data.ErrorMessage}"
    : $"Cancel request failed: {cancel.Error}");
