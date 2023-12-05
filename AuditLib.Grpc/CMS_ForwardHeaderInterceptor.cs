using Grpc.Core;
using Grpc.Core.Interceptors;
using Helper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuditLib.Grpc
{
    public class CMSForwardHeaderInterceptor : Interceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProvider _redisProvider;
        private readonly ILogger<CMSForwardHeaderInterceptor> _logger;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public CMSForwardHeaderInterceptor(IHttpContextAccessor httpContextAccessor
            , ITokenProvider redisProvider
            , ILogger<CMSForwardHeaderInterceptor> logger
            , AuthenticationStateProvider authenticationStateProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _redisProvider = redisProvider;
            _logger = logger;
            _authenticationStateProvider = authenticationStateProvider;
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
                var authenticationState = _authenticationStateProvider.GetAuthenticationStateAsync().Result;
                var user = authenticationState.User;
                var sessionId = user?.Claims.Where(x => x.Type == "sessionId").Select(c => c.Value).SingleOrDefault();
                if (!string.IsNullOrEmpty(sessionId))
                {
                    var token = _redisProvider.Get<string>(sessionId);
                    if (!string.IsNullOrEmpty(token))
                    {
                        newOptions.Headers!.Add("Authorization", $"Bearer {token}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CMSForwardHeaderInterceptor", ex);
            }


            return base.AsyncUnaryCall(request, newContext, continuation);
            //var call = continuation(request, newContext);
            //return new AsyncUnaryCall<TResponse>(
            //    HandleResponse(call.ResponseAsync),
            //    call.ResponseHeadersAsync,
            //    call.GetStatus,
            //    call.GetTrailers,
            //    call.Dispose); 
        }


        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> inner)
        {
            try
            {
                var rs = await inner;
                return rs;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Custom error", ex);
            }
        }
    }
}
