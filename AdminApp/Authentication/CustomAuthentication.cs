using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AdminApp.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly ILogger<CustomAuthentication> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public CustomAuthentication(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claims = _httpContextAccessor.HttpContext.User.Identity;
            var claimsPrincipal = new ClaimsPrincipal(claims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}       
