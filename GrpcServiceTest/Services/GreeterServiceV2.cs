using Grpc.Core;

namespace GrpcServiceTest.Services;

public class GreeterServiceV2 : Greeter.GreeterBase
{
    private readonly ILogger<GreeterServiceV2> _logger;
    public GreeterServiceV2(ILogger<GreeterServiceV2> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

}
