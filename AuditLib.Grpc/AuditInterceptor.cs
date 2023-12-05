using Audit.Core;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Helper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AuditLib.Grpc
{
    public sealed class AuditInterceptor : Interceptor
    {
        private readonly IAuditHelper _auditHelper;
        private readonly AuditCore _auditCore;
        private readonly IIpHelper _ipHelper;
        private readonly ILogger<AuditInterceptor> _logger;
        private static readonly string? _assemblyName;
        static AuditInterceptor()
        {
            try
            {
                _assemblyName = AppDomain.CurrentDomain.GetAssemblies()[1].FullName ?? string.Empty;
            }
            catch { }
        }
        public AuditInterceptor(ILogger<AuditInterceptor> logger, IIpHelper ipHelper, IAuditHelper auditHelper, AuditCore auditCore)
        {
            _auditHelper = auditHelper;
            _auditCore = auditCore;
            _logger = logger;
            _ipHelper = ipHelper;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var isAudit = _auditHelper.IsAudit(context.Method);
            if (!isAudit)
            {
                return await continuation(request, context);
            }
            var isAuditRequest = _auditHelper.IsAuditRequest(context.Method);
            var isAuditResponse = _auditHelper.IsAuditResponse(context.Method);

            var httpContext = context.GetHttpContext();
            if (!httpContext.Request.Headers.TryGetValue("clientip", out _))
                httpContext.Request.Headers.Add("clientip", _ipHelper.GetClientIp(httpContext));
            if (!httpContext.Request.Headers.TryGetValue("traceid", out var traceid))
            {
                traceid = httpContext.TraceIdentifier;
                httpContext.Request.Headers.Add("traceid", traceid);
            }
            var auditAction = new CustomAuditEvent
            {
                UserName = httpContext.User?.Identity?.Name,
                IpAddress = httpContext.Connection?.RemoteIpAddress?.ToString(),
                TraceId = traceid,
                FormVariables = httpContext.Request.HasFormContentType ? _auditHelper.GetVariables(httpContext.Request.Form) : null,
                Headers = _auditHelper.GetHeaders(httpContext.Request.Headers),
                RequestBody = new
                {
                    Type = httpContext.Request.ContentType,
                    Length = httpContext.Request.ContentLength,
                    Value = isAuditRequest ? request : null
                }
            };
            using var auditScope = await AuditScope.CreateAsync(new AuditScopeOptions
            {
                EventType = $"{context.Host}{context.Method}",
                AuditEvent = auditAction,
                CreationPolicy = EventCreationPolicy.Manual,
            });
            _auditCore.CurrentScope = auditScope;
            auditScope.Event.Environment.CallingMethodName = context.Method;
            auditScope.Event.Environment.AssemblyName = _assemblyName;
            Status status;
            try
            {
                var response = await continuation(request, context);
                auditAction.ResponseBody = new
                {
                    Value = isAuditResponse ? response : null
                };
                return response;
            }
            catch (RpcException ex)
            {
                auditAction.Exception = $"An error occured when calling {context.Method}. RpcException: {ex.Message}";
                status = ex.Status;
                auditAction.ResponseBody = new
                {
                    Value = status.ToString()
                };
                throw new RpcException(status);
            }
            catch (Exception ex)
            {
                status = new Status(StatusCode.Unavailable, "Unavailable");
                auditAction.Exception = $"An error occured when calling {context.Method}. Exception: {ex.Message}";
                auditAction.ResponseBody = new
                {
                    Value = status.ToString()
                };
                throw new RpcException(status);
            }
            finally
            {
                try
                {
                    await auditScope.SaveAsync();
                    await auditScope.DisposeAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "AuditInterceptor SaveAsync");
                }
            }

        }
    }
}
