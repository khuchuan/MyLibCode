using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditLib.Grpc
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAuditLogGrpc(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuditLog(configuration);
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddTransient<IIpHelper, IpHelper>();
            serviceCollection.AddGrpc(options =>
            {
                options.Interceptors.Add<AuditInterceptor>();
            });
            return serviceCollection;
        }
    }
}
