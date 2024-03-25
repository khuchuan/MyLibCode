using GrpcServer;
using static GrpcServer.Greeter;

namespace GrpcClient;

public class GreaterClientService: IGreaterClientService
{
    private readonly GreeterClient _client;

    public GreaterClientService(GreeterClient client)
    {
        _client = client;
    }
    
    public string CallSayHello(string msg)
    {
        var reply = _client.SayHello(new HelloRequest { Name = msg });
        return reply.Message;
    }
}

public interface IGreaterClientService
{
    string CallSayHello(string msg);
}

