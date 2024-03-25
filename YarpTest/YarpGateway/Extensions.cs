using Yarp.ReverseProxy.Transforms;

namespace YarpGateway
{
    internal static class Extensions
    {
        public static IServiceCollection AddReverseProxy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddReverseProxy()
                    .LoadFromConfig(configuration.GetSection("ServerReverseProxy"))
            .AddTransforms(builderContext =>
            {
                // Added to all routes.
                builderContext.AddResponseHeaderRemove(headerName: "Server", ResponseCondition.Always);
                builderContext.AddResponseHeaderRemove(headerName: "X-Powered-By", ResponseCondition.Always);
            }).ConfigureHttpClient((context, handler) =>
            {
                handler.SslOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, chainErrors) => true;
                handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;

            });
            return services;
        }

        public static WebApplicationBuilder AddFileConfigs(this WebApplicationBuilder builder)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/ConfigurationProxy/";
                var listfiles = Directory.EnumerateFiles(path, $"*.json", SearchOption.AllDirectories);
                var listfile_default = listfiles.Where(c => c.Split('/').Last().Split(".").Length == 2).ToList();
                foreach (var jsonFilename in listfile_default)
                    builder.Configuration.AddJsonFile(jsonFilename, optional: true, reloadOnChange: false);
                foreach (var jsonFilename in listfiles.Where(c => c.Split('/').Last().Split(".").Length > 2 && c.Split('/').Last().Split(".").Contains(builder.Environment.EnvironmentName)))
                    builder.Configuration.AddJsonFile(jsonFilename, optional: true, reloadOnChange: true);
            }
            catch (Exception ex)
            {
                return builder;
            }
            return builder;
        }

    }
}
