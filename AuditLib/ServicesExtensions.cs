using Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuditLib
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuditLog(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<AuditLogConfig>(configuration);
            serviceCollection.AddSingleton<IAuditHelper, AuditHelper>();
            serviceCollection.AddScoped<AuditCore>();

            serviceCollection.AddSingleton<AuditCustomDataProvider>();
            serviceCollection.AddHostedService<QueuedHostedService>();
            serviceCollection.AddSingleton<IAuditBackgroundTaskQueue>(ctx =>
            {
                return new BackgroundTaskQueue(10000);
            });
            return serviceCollection;
        }

        public static IApplicationBuilder UseAuditLog(this IApplicationBuilder builder)
        {
            builder.ApplicationServices.GetService<IAuditHelper>();
            return builder;
        }

        public static IHost UseAuditLog(this IHost builder)
        {
            builder.Services.GetRequiredService<IAuditHelper>();
            return builder;
        }
    }
}
