//var builder = WebApplication.CreateBuilder(args);

//AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

//var xx = builder.Configuration.GetSection("ReverseProxy");

//builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
//    .ConfigureHttpClient((context, handler) =>
//    {
//        handler.SslOptions.RemoteCertificateValidationCallback = (sender, certificate, chain, chainErrors) => true;
//        handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
//    });

//var app = builder.Build();
//app.MapReverseProxy();

//app.Run();


using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Yarp.ReverseProxy;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

// Add services required for reverse proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the reverse proxy pipeline
app.UseEndpoints(endpoints =>
{
    endpoints.MapReverseProxy();
});

// Define the configuration for reverse proxy
app.add(proxy =>
{
    // Example configuration for proxying gRPC service
    proxy.Add("grpcService", createProxyPipeline: context =>
    {
        var destinationPrefix = "https://your-grpc-service.com";
        var grpcOptions = new GrpcTransportSettings
        {
            Address = destinationPrefix,
            Forwarder = context.GetRequiredService<HttpForwarder>(),
        };
        return Task.FromResult<ProxyRoute>(new ProxyRoute()
        {
            ClusterId = destinationPrefix,
            DownstreamScheme = "https",
            DownstreamPathTemplate = "/{**catch-all}",
            Transforms =
            {
                new HttpTransformBuilder().WithRequestTransform(async (proxyReq, _) =>
                {
                    proxyReq.RequestUri = new UriBuilder(proxyReq.RequestUri)
                    {
                        Scheme = grpcOptions.Forwarder.Address.Scheme,
                        Port = grpcOptions.Forwarder.Address.Port,
                        Path = grpcOptions.Forwarder.Address.AbsolutePath,
                    }.Uri;
                    proxyReq.Headers.Add(HeaderNames.Host, destinationPrefix);
                }).Build(),
            },
            Metadata = grpcOptions,
        });
    });
});

app.Run();
