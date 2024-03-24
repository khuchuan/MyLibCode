using GrpcServer;
using Grpc.Net.Client;

namespace ClientConsumerGrpc;

public class CallGrpc
{    
    public void FuncSayHello(string name, string url)
    {
        string msg = string.Empty;
        try
        {
            //using var channel = GrpcChannel.ForAddress(url);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel =  GrpcChannel.ForAddress(url);

            var client = new Greeter.GreeterClient(channel);
            var request = new HelloRequest { Name = name };
            var response = client.SayHello(request);
            msg = response.Message;
        }
        catch (Exception ex)
        {
            msg = "Error:" + ex.Message;
        }        
        Console.WriteLine(msg +"\n" + url);
    }
}
