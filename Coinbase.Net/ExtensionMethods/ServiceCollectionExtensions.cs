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
using Coinbase.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services such as the ICoinbaseRestClient and ICoinbaseSocketClient. Configures the services based on the provided configuration.
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="configuration">The configuration(section) containing the options</param>
        /// <returns></returns>
        public static IServiceCollection AddCoinbase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var options = new CoinbaseOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            configuration.Bind(options);

            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            var restEnvName = options.Rest.Environment?.Name ?? options.Environment?.Name ?? CoinbaseEnvironment.Live.Name;
            var socketEnvName = options.Socket.Environment?.Name ?? options.Environment?.Name ?? CoinbaseEnvironment.Live.Name;
            options.Rest.Environment = CoinbaseEnvironment.GetEnvironmentByName(restEnvName) ?? options.Rest.Environment!;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = CoinbaseEnvironment.GetEnvironmentByName(socketEnvName) ?? options.Socket.Environment!;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;


            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

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
            var options = new CoinbaseOptions();
            // Reset environment so we know if theyre overriden
            options.Rest.Environment = null!;
            options.Socket.Environment = null!;
            optionsDelegate?.Invoke(options);
            if (options.Rest == null || options.Socket == null)
                throw new ArgumentException("Options null");

            options.Rest.Environment = options.Rest.Environment ?? options.Environment ?? CoinbaseEnvironment.Live;
            options.Rest.ApiCredentials = options.Rest.ApiCredentials ?? options.ApiCredentials;
            options.Socket.Environment = options.Socket.Environment ?? options.Environment ?? CoinbaseEnvironment.Live;
            options.Socket.ApiCredentials = options.Socket.ApiCredentials ?? options.ApiCredentials;

            services.AddSingleton(x => Options.Options.Create(options.Rest));
            services.AddSingleton(x => Options.Options.Create(options.Socket));

            return AddCoinbaseCore(services, options.SocketClientLifeTime);
        }

        /// <summary>
        /// DEPRECATED; use <see cref="AddCoinbase(IServiceCollection, Action{CoinbaseOptions}?)" /> instead
        /// </summary>
        public static IServiceCollection AddCoinbase(
            this IServiceCollection services,
            Action<CoinbaseRestOptions> restDelegate,
            Action<CoinbaseSocketOptions>? socketDelegate = null,
            ServiceLifetime? socketClientLifeTime = null)
        {
            services.Configure<CoinbaseRestOptions>((x) => { restDelegate?.Invoke(x); });
            services.Configure<CoinbaseSocketOptions>((x) => { socketDelegate?.Invoke(x); });

            return AddCoinbaseCore(services, socketClientLifeTime);
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
                var handler = new HttpClientHandler();
                try
                {
                    handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                }
                catch (PlatformNotSupportedException)
                { }

                var options = serviceProvider.GetRequiredService<IOptions<CoinbaseRestOptions>>().Value;
                if (options.Proxy != null)
                {
                    handler.Proxy = new WebProxy
                    {
                        Address = new Uri($"{options.Proxy.Host}:{options.Proxy.Port}"),
                        Credentials = options.Proxy.Password == null ? null : new NetworkCredential(options.Proxy.Login, options.Proxy.Password)
                    };
                }
                return handler;
            });
            services.Add(new ServiceDescriptor(typeof(ICoinbaseSocketClient), x => { return new CoinbaseSocketClient(x.GetRequiredService<IOptions<CoinbaseSocketOptions>>(), x.GetRequiredService<ILoggerFactory>()); }, socketClientLifeTime ?? ServiceLifetime.Singleton));

            services.AddTransient<ICryptoRestClient, CryptoRestClient>();
            services.AddSingleton<ICryptoSocketClient, CryptoSocketClient>();
            services.AddTransient<ICoinbaseOrderBookFactory, CoinbaseOrderBookFactory>();
            services.AddTransient<ICoinbaseTrackerFactory, CoinbaseTrackerFactory>();

            services.RegisterSharedRestInterfaces(x => x.GetRequiredService<ICoinbaseRestClient>().AdvancedTradeApi.SharedClient);
            services.RegisterSharedSocketInterfaces(x => x.GetRequiredService<ICoinbaseSocketClient>().AdvancedTradeApi.SharedClient);

            return services;
        }
    }
}
