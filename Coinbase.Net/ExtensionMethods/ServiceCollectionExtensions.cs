using Coinbase.Net;
using Coinbase.Net.Clients;
using Coinbase.Net.Interfaces;
using Coinbase.Net.Interfaces.Clients;
using Coinbase.Net.Objects.Options;
using Coinbase.Net.SymbolOrderBooks;
using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the ICoinbaseRestClient and ICoinbaseSocketClient. Configures the services based on the provided configuration.<br />
        /// See <see href="https://github.com/JKorf/Coinbase.Net/blob/main/Examples/example-config.json" /> for an example of how to set up the configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddCoinbase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = CoinbaseOptions.CreateFromConfiguration(configuration);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddCoinbaseCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// Add services such as the ICoinbaseRestClient and ICoinbaseSocketClient. Services will be configured based on the provided options.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="optionsDelegate">Set options for the Coinbase services</param>
        /// <returns></returns>
        public static IServiceCollection AddCoinbase(
            this IServiceCollection services,
            Action<CoinbaseOptions>? optionsDelegate = null)
        {
            var options = CoinbaseOptions.Create(optionsDelegate);

            services.AddSingleton(Options.Options.Create(options.Rest));
            services.AddSingleton(Options.Options.Create(options.Socket));
            services.AddSingleton(Options.Options.Create(options));

            return AddCoinbaseCore(services, options.SocketClientLifeTime);
        }

        private static IServiceCollection AddCoinbaseCore(
            this IServiceCollection services,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.AddHttpClient<ICoinbaseRestClient, CoinbaseRestClient>((client, serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<CoinbaseRestOptions>>().Value;
                client.Timeout = options.RequestTimeout;
                return new CoinbaseRestClient(client, serviceProvider.GetRequiredService<ILoggerFactory>(), serviceProvider.GetRequiredService<IOptions<CoinbaseRestOptions>>());
            }).ConfigurePrimaryHttpMessageHandler((serviceProvider) => {
                var options = serviceProvider.GetRequiredService<IOptions<CoinbaseRestOptions>>().Value;
                return LibraryHelpers.CreateHttpClientMessageHandler(options);
            }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
            services.Add(new ServiceDescriptor(typeof(ICoinbaseSocketClient), x => { return new CoinbaseSocketClient(x.GetRequiredService<IOptions<CoinbaseSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICoinbaseOrderBookFactory, CoinbaseOrderBookFactory>();
            services.AddTransient<ICoinbaseTrackerFactory, CoinbaseTrackerFactory>();
            services.AddTransient<ITrackerFactory, CoinbaseTrackerFactory>();
            services.AddSingleton<ICoinbaseUserClientProvider, CoinbaseUserClientProvider>(x =>
            new CoinbaseUserClientProvider(
                x.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(ICoinbaseRestClient).Name),
                x.GetRequiredService<ILoggerFactory>(),
                x.GetRequiredService<IOptions<CoinbaseRestOptions>>(),
                x.GetRequiredService<IOptions<CoinbaseSocketOptions>>()));

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICoinbaseRestClient>().AdvancedTradeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICoinbaseSocketClient>().AdvancedTradeApi.SharedClient);

            services.RegisterSharedApiClient<
                ICoinbaseSharedApiClient,
                CoinbaseSharedApiClient>(sharedApis => sharedApis
                    .Add(client => client.AdvancedTradeRest)
                    .Add(client => client.AdvancedTradeSocket)
                    );

            return services;
        }
    }
}
