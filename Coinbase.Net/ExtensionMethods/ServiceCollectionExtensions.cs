using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using Coinbase.Net.Clients;
using Coinbase.Net.Interfaces;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.SymbolOrderBooks;
using CryptoExchange.Net;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the ICoinbaseRestClient and ICoinbaseSocketClient to the sevice collection so they can be injected
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="defaultRestOptionsDelegate">Set default options for the rest client</param>
        /// <param name="defaultSocketOptionsDelegate">Set default options for the socket client</param>
        /// <param name="socketClientLifeTime">The lifetime of the ICoinbaseSocketClient for the service collection. Defaults to Singleton.</param>
        /// <returns></returns>
        public static IServiceCollection AddCoinbase(
            this IServiceCollection services,
            Action<CoinbaseRestOptions>? defaultRestOptionsDelegate = null,
            Action<CoinbaseSocketOptions>? defaultSocketOptionsDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            var restOptions = CoinbaseRestOptions.Default.Copy();

            if (defaultRestOptionsDelegate != null)
            {
                defaultRestOptionsDelegate(restOptions);
                CoinbaseRestClient.SetDefaultOptions(defaultRestOptionsDelegate);
            }

            if (defaultSocketOptionsDelegate != null)
                CoinbaseSocketClient.SetDefaultOptions(defaultSocketOptionsDelegate);

            services.AddHttpClient<ICoinbaseRestClient, CoinbaseRestClient>(options =>
            {
                options.Timeout = restOptions.RequestTimeout;
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (restOptions.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{restOptions.Proxy.Host}:{restOptions.Proxy.Port}"),
                        Credentials = restOptions.Proxy.Password == null ? null : new NetworkCredential(restOptions.Proxy.Login, restOptions.Proxy.Password)
                    };
                }
                return handler;
            });

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<ICoinbaseOrderBookFactory, CoinbaseOrderBookFactory>();

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICoinbaseRestClient>().AdvancedTradeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICoinbaseSocketClient>().AdvancedTradeApi.SharedClient);

            if (socketClientLifeTime == null)
                services.AddSingleton<ICoinbaseSocketClient, CoinbaseSocketClient>();
            else
                services.Add(new ServiceDescriptor(typeof(ICoinbaseSocketClient), typeof(CoinbaseSocketClient), socketClientLifeTime.Value));
            return services;
        }
    }
}
