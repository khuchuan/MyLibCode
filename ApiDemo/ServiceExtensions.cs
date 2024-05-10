using Infrastructure;
using Infrastructure.Policies;

namespace ApiDemo;

public static class ServiceExtensions
{
    public static void ConfigureHttpClientService(this IServiceCollection services)
    {
        services.AddHttpClient("").UseCircuitBreakerPolicy();
    }

 
}
