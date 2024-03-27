using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using System.Net.Security;
using System.Threading.Channels;
using static GrpcServer.Greeter;

namespace ClientConsumerGrpc;

public class CallGrpc
{
    public void FuncSayHello(string name, string url)
    {
        string msg = string.Empty;
        try
        {
            var sslOptions = new SslClientAuthenticationOptions
            {
                // Leave certs unvalidated for debugging
                RemoteCertificateValidationCallback = delegate { return true; },
            };

            var configSocketHttpHandler = new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                AllowAutoRedirect = false,
                UseProxy = false,
                SslOptions = sslOptions
            };

            var chanelOptions = new GrpcChannelOptions
            {
                HttpHandler = configSocketHttpHandler
                //, Credentials = ChannelCredentials.Insecure
            };

            using var channel = GrpcChannel.ForAddress(url, chanelOptions);

            var client = new GreeterClient(channel);
            var request = new HelloRequest { Name = name };

            var response = client.WithHost(url).SayHello(request);

            msg = response.Message;
        }
        catch (Exception ex)
        {
            msg = "Error:" + ex.Message;
        }
        Console.WriteLine(msg + "\n" + url);
    }
}
