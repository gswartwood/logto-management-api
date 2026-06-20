using Logto.ManagementApi.Auth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Logto.ManagementApi.Utils;

public static class IServiceCollectionExtensions
{
    extension(IServiceCollection services)
    { 
        public IServiceCollection AddLogtoMgmtApiClient(Action<ApiClientOptions> options)
        {
            services.Configure(options);

            services.AddHttpClient(LogtoM2MTokenHandler.AuthClientName);

            services.AddTransient<LogtoM2MTokenHandler>();
            services.AddHttpClient<ILogtoMgmtApiClient, LogtoMgmtApiClient>()
                .ConfigureHttpClient((sp, client) =>
                {
                    var opts = sp.GetRequiredService<IOptions<ApiClientOptions>>().Value;
                    client.BaseAddress = new Uri(opts.BaseAddress);
                })
                .AddHttpMessageHandler<LogtoM2MTokenHandler>();

            return services;
        }
    }
}
