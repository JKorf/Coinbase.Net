using Coinbase.Net;
using Coinbase.Net.Clients;
using Coinbase.Net.Enums;

const string symbol = "BTC-USDC";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var privateKey = "PRIVATEKEY";

Console.WriteLine("Coinbase.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new CoinbaseRestClient(options =>
{
    options.ApiCredentials = new CoinbaseCredentials()
        .WithECDsa(apiKey, privateKey);
});

await PlaceSpotLimitOrderAsync(client);

static async Task PlaceSpotLimitOrderAsync(CoinbaseRestClient client)
{
    Console.WriteLine($"Placing Advanced Trade limit buy order for {symbol}...");

    var ticker = await client.AdvancedTradeApi.ExchangeData.GetSymbolAsync(symbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get ticker: {ticker.Error}");
        return;
    }

    var lastPrice = ticker.Data.LastPrice ?? ticker.Data.MidMarketPrice;
    if (lastPrice == null)
    {
        Console.WriteLine("Failed to get ticker price");
        return;
    }

    var safePrice = Math.Round(lastPrice.Value * 0.95m, 2);
    var order = await client.AdvancedTradeApi.Trading.PlaceOrderAsync(
        symbol: symbol,
        side: OrderSide.Buy,
        orderType: NewOrderType.Limit,
        quantity: 0.00001m,
        price: safePrice);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed order {order.Data.SuccessResponse.OrderId}");

    var orderStatus = await client.AdvancedTradeApi.Trading.GetOrderAsync(order.Data.SuccessResponse.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Order status: {orderStatus.Data.OrderStatus}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query order: {orderStatus.Error}");

    var cancel = await client.AdvancedTradeApi.Trading.CancelOrderAsync(order.Data.SuccessResponse.OrderId);
    Console.WriteLine(cancel.Success && cancel.Data.Success
        ? $"Cancelled order {order.Data.SuccessResponse.OrderId}"
        : $"Failed to cancel order: {cancel.Error?.ToString() ?? cancel.Data.ErrorMessage}");
}
