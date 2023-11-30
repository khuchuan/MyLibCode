using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Helper
{
    public sealed class IpHelper : IIpHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<IpHelper> _logger;
        public IpHelper(IHttpContextAccessor httpContextAccessor, ILogger<IpHelper> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public string GetClientIp(HttpContext? httpContext = null)
        {
            string IP = string.Empty;
            try
            {
                httpContext ??= _httpContextAccessor.HttpContext;
                if (httpContext.Request.Headers.TryGetValue("HTTP_X_FORWARDED_FOR", out var HTTP_X_FORWARDED_FOR))
                    IP = HTTP_X_FORWARDED_FOR.ToString();
                else if (httpContext.Request.Headers.TryGetValue("HTTP_CITRIX", out var HTTP_CITRIX))
                    IP = HTTP_CITRIX.ToString();
                else if (httpContext.Request.Headers.TryGetValue("CITRIX_CLIENT_HEADER", out var CITRIX_CLIENT_HEADER))
                    IP = CITRIX_CLIENT_HEADER.ToString();
                if (string.IsNullOrEmpty(IP))
                {
                    IP = httpContext.Connection.RemoteIpAddress.ToString();
                }
                else
                {
                    var ips = IP.Split(',', ';');
                    if (ips.Length > 0)
                        IP = ips[0];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetClientIp");
            }
            return IP.Replace('|', '#').Trim();
        }
    }

    public interface IIpHelper
    {
        string GetClientIp(HttpContext? httpContext = null);
    }
}
