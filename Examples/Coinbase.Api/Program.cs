using Coinbase.Net;
using Coinbase.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the Coinbase services
builder.Services.AddCoinbase();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddCoinbase(options =>
{
    options.ApiCredentials = new CoinbaseCredentials()
        .WithECDsa("API_KEY", "PRIVATE_KEY");
    options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoint and inject the rest client
app.MapGet("/{Symbol}", async ([FromServices] ICoinbaseRestClient client, string symbol) =>
{
    var result = await client.AdvancedTradeApi.ExchangeData.GetSymbolAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data.LastPrice)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] ICoinbaseRestClient client) =>
{
    var result = await client.AdvancedTradeApi.Account.GetAccountsAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
