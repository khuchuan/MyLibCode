using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AuditLib.Grpc
{
    public class ForwardHeaderInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ForwardHeaderInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)

        {
            var newOptions = context.Options.WithHeaders(context.Options.Headers ?? new Metadata());
            var traceId = _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(x => x.Key.Equals("traceid")).Value;
            if (!string.IsNullOrEmpty(traceId))
            {
                newOptions.Headers!.Add("traceid", traceId);
            }
            var clientIp = _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault(x => x.Key.Equals("clientip")).Value;
            if (!string.IsNullOrEmpty(clientIp))
            {
                newOptions.Headers!.Add("clientip", clientIp);
            }
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, newOptions);
            try
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var loginToken = user?.Identity?.Name;
                var token = user?.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();

                if (!string.IsNullOrEmpty(loginToken))
                {
                    newOptions.Headers!.Add("Authorization", $"Bearer {token}");
                }
            }
            catch
            {

            }
            return base.AsyncUnaryCall(request, newContext, continuation);

        }
    }
}
