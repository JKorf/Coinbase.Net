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
   options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>");
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
    return result.Data.LastPrice;
})
.WithOpenApi();


app.MapGet("/Balances", async ([FromServices] ICoinbaseRestClient client) =>
{
    var result = await client.AdvancedTradeApi.Account.GetAccountsAsync();
    return (object)(result.Success ? result.Data : result.Error!);
})
.WithOpenApi();

app.Run();