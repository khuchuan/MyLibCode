using Helper;

namespace AdminApp.Services
{
    public interface IRefreshTokenRedis
    {
        public Task RefreshInRedis(string key, string token, TimeSpan expired);
    }
    public class RefreshTokenRedis : IRefreshTokenRedis
    {
        private readonly ITokenProvider _redisProvider;
        public RefreshTokenRedis(ITokenProvider redisProvider)
        {
            _redisProvider = redisProvider;
        }
        public async Task RefreshInRedis(string key, string token, TimeSpan expired)
        {
            await _redisProvider.SetAsync(key, token, expired);
        }
    }
}
