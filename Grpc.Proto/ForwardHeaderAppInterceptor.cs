using Grpc.Core;
using Grpc.Core.Interceptors;
using Helper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

namespace GrpcLib.Proto
{
    public sealed class ForwardHeaderAppInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDataProtector _protector;
        public ForwardHeaderAppInterceptor(IHttpContextAccessor httpContextAccessor, ITokenProvider tokenProvider, IDataProtectionProvider dataProtectionProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
            _protector = dataProtectionProvider.CreateProtector("accesstoken");
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)

        {
            CallOptions newOptions = GetCallOptions(context);
            var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, newOptions);
            return continuation(request, newContext);
        }

        private CallOptions GetCallOptions<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context)
            where TRequest : class
            where TResponse : class
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
            if (!newOptions.Headers!.Any(x => x.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)) && _httpContextAccessor.HttpContext?.User.Identity != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var userName = _httpContextAccessor.HttpContext?.User.Identity.Name;
                var cacheProtect = _tokenProvider.GetString($"Token:{userName}");
                if (!string.IsNullOrEmpty(cacheProtect))
                {
                    var cacheUnprotect = _protector.Unprotect(cacheProtect);
                    newOptions.Headers!.Add("Authorization", $"Bearer {cacheUnprotect}");
                }
            }
            return newOptions;
        }

    }
}
