using GrpcServer;
using System.Net.Security;

namespace GrpcClient.Grpc;


public static class GrpcServiceCollection
{
    public static void AddGrpcService(this IServiceCollection services, ConfigServiceUrl setting = null)
    {
        var sslOptions = new SslClientAuthenticationOptions
        {
            // Leave certs unvalidated for debugging
            RemoteCertificateValidationCallback = delegate { return true; },
        };
        //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); // user http
        var configSocketHttpHandler = new SocketsHttpHandler
        {
            EnableMultipleHttp2Connections = true,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            AllowAutoRedirect = false,
            UseProxy = false,
            SslOptions = sslOptions
        };

        #region Service config
        var coreSetting = setting;
        bool GreeterService()
        {
            var grpc_url = new Uri($"{coreSetting.GatewayApi}/server-grpc");
            services.AddGrpcClient<Greeter.GreeterClient>(o =>
            {
                o.Address = grpc_url;
                //o.ChannelOptionsActions.Add(channelOptions => channelOptions.Credentials = ChannelCredentials.Insecure);
            }).AddInterceptor<GrpcInterceptor>().EnableCallContextPropagation(o => o.SuppressContextNotFoundErrors = true).ConfigureChannel(o =>
            {
                o.HttpHandler = grpc_url.Segments.Length > 1 ? new SubdirectoryHandler(configSocketHttpHandler, String.Join("", grpc_url.Segments)) : configSocketHttpHandler;
                o.HttpClient = null;
                //o.UnsafeUseInsecureChannelCallCredentials = true;
            });
            return true;
        }

        GreeterService();
     
        
        #endregion
    }

}



public class ConfigServiceUrl
{
    public string GatewayApi { get; set; }
}

public class SubdirectoryHandler : DelegatingHandler
{
    private readonly string _subdirectory;

    public SubdirectoryHandler(HttpMessageHandler innerHandler, string subdirectory)
        : base(innerHandler)
    {
        _subdirectory = subdirectory;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var old = request.RequestUri;

        var url = $"{old.Scheme}://{old.Host}:{old.Port}";
        url += $"{_subdirectory}{request.RequestUri.AbsolutePath}";
        request.RequestUri = new Uri(url, UriKind.Absolute);

        return base.SendAsync(request, cancellationToken);
    }
}