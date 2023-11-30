using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Helper;


namespace AdminApp.Models
{
    public class ApplicationSignInManager : SignInManager<IdentityUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ITokenProvider _redisProvider;
        public ApplicationSignInManager(UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            ITokenProvider redisProvider,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<IdentityUser>> logger,
            IAuthenticationSchemeProvider schemeProvider, IUserConfirmation<IdentityUser> userConfirmation, IConfiguration configuration
        ) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, userConfirmation)
        {
            _contextAccessor = contextAccessor;
            _redisProvider = redisProvider;
            _configuration = configuration;
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            string uri = _configuration["Authen:Url"];
            string domainCookie = _configuration["Authen:DomainCookie"];
            string clientId = _configuration["Authen:AudienceId"];
            var jwtProvider = JWTFrontEnd.Providers.JwtProvider.Create(uri);
            var token = await jwtProvider.GetTokenAsync(userName, password, clientId);
            var sessionId = Guid.NewGuid().ToString();
            if (token == null)
            {
                return SignInResult.Failed;
            }
            else
            {
                //create an Identity Claim
                ClaimsIdentity claims = JWTFrontEnd.Providers.JwtProvider.CreateIdentity(true, userName, token.access_token, token.refresh_token, sessionId);
                //sign in
                await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims), new AuthenticationProperties
                {
                    IsPersistent = isPersistent,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddSeconds(token.expires_in)
                });

                await _redisProvider.SetAsync(sessionId, token.access_token, TimeSpan.FromSeconds(token.expires_in));
                _contextAccessor.HttpContext.Response.Cookies.Append("refresh_token", string.IsNullOrEmpty(token.refresh_token) ? "" : token.refresh_token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(14),
                    Domain = domainCookie,
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                });
                _contextAccessor.HttpContext.Response.Cookies.Append("access_token", token.access_token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddSeconds(token.expires_in),
                    Domain = domainCookie,
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                });
                return SignInResult.Success;
            }
        }
    }
}
