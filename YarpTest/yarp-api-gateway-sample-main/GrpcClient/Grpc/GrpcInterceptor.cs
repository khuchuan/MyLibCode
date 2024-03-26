using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using static Grpc.Core.Interceptors.Interceptor;

namespace GrpcClient.Grpc;

public class GrpcInterceptor : Interceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GrpcInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
      TRequest request,
      ClientInterceptorContext<TRequest, TResponse> context,
      AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var accessToken = _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        //LogCall(context.Method);
        //string Path = context.Method.FullName;
        return continuation(request, context);
    }

    private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t, string request)
    {
        try
        {
            var response = await t;
            return response;
        }
        catch (RpcException ex)
        {
            throw;
        }
    }
}