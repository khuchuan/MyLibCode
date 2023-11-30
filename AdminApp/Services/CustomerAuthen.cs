using Helper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
namespace AdminApp.Services
{
    public class CustomAuthSchemeOptions : AuthenticationSchemeOptions { }

    public class CustomAuthHandler : AuthenticationHandler<CustomAuthSchemeOptions>
    {
        private readonly ILogger<CustomAuthHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ITokenProvider _redisProvider;
        public CustomAuthHandler(
            IOptionsMonitor<CustomAuthSchemeOptions> options,
            ILoggerFactory loggerFactory,
            ILogger<CustomAuthHandler> logger,
            IHttpContextAccessor httpContextAccessor,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration,
            ITokenProvider redisProvider)
            : base(options, loggerFactory, encoder, clock)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _redisProvider = redisProvider;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.User;

                if (user == null || !user.Identity.IsAuthenticated)
                {
                    return AuthenticateResult.NoResult();
                }
                var sessionId = user.Claims.Where(x => x.Type == "sessionId").Select(c => c.Value).SingleOrDefault();
                if (string.IsNullOrEmpty(sessionId))
                {
                    return AuthenticateResult.Fail("SessionId Null.");
                }

                var token = _redisProvider.Get<string>(sessionId);
                if (string.IsNullOrEmpty(token))
                {
                    var refreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refresh_token"];
                    var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
                    {
                        Address = _configuration["Authen:Url"],
                        RefreshToken = refreshToken,
                        ClientId = _configuration["Authen:AudienceId"]
                    });
                    if (response.IsError)
                    {
                        return AuthenticateResult.Fail("Token Null.");
                    }
                    _ = _redisProvider.SetAsync(sessionId, response.AccessToken, TimeSpan.FromSeconds(response.ExpiresIn));
                }
                var claimsIdentity = new ClaimsIdentity(user.Claims, nameof(CustomAuthHandler));
                var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HandleAuthenticateAsync");
                return AuthenticateResult.Fail("Exception.");
            }
        }
    }
}
