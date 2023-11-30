using Serilog;
using StackExchange.Redis;
using System.Text.Json;

namespace Helper
{


    public interface IRedisProvider
    {
        Task SetAsync<T>(string key, T value);
        Task SetAsync<T>(string key, T value, TimeSpan timeout);
        Task<T?> GetAsync<T>(string key);
        public T? Get<T>(string key);
        Task<bool> RemoveAsync(string key);

        Task<bool> IsInCacheAsync(string key);

        Task<bool> SetIfNotExistAsync(string key, TimeSpan timeout);
        Task<bool> SetAsync(string key, string value, TimeSpan expiry, CommandFlags flags = CommandFlags.FireAndForget);
        Task<string?> GetStringAsync(string key);
        string? GetString(string key);
    }
    public interface ICheckDuplicateTrackIdProvider : IRedisProvider
    {

    }
    public interface IPromotionProvider : IRedisProvider
    {

    }
    public interface ITokenProvider : IRedisProvider
    {

    }
    public class RedisProvider : IRedisProvider, IPromotionProvider, ITokenProvider, ICheckDuplicateTrackIdProvider
    {
        private readonly ConnectionMultiplexer _redisClient;
        public RedisProvider(string connectionString)
        {
            _redisClient = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            await SetAsync(key, value, TimeSpan.Zero);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan timeout)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                await db.StringSetAsync(key, JsonSerializer.Serialize(value), timeout, When.Always, CommandFlags.FireAndForget);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error set value for redis");
                throw;
            }
        }
        public async Task<bool> SetAsync(string key, string value, TimeSpan expiry, CommandFlags flags = CommandFlags.FireAndForget)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                return await db.StringSetAsync(key, value, expiry, When.Always, flags);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error set value for redis");
                return false;
            }
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            T? result = default;
            try
            {
                var db = _redisClient.GetDatabase();
                var data = await db.StringGetAsync(key);
                if (!string.IsNullOrEmpty(data))
                    result = JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error get value for redis ");
                throw;
            }
            return result;
        }
        public async Task<string?> GetStringAsync(string key)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                return await db.StringGetAsync(key);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error get value for redis ");
                throw;
            }
        }
        public string? GetString(string key)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                return db.StringGet(key);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error get value for redis ");
                throw;
            }
        }
        public T? Get<T>(string key)
        {
            T? result = default;
            try
            {
                var db = _redisClient.GetDatabase();
                var data = db.StringGet(key);
                if (!string.IsNullOrEmpty(data))
                    result = JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error get value for redis ");
                throw;
            }
            return result;
        }
        public async Task<bool> RemoveAsync(string key)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                await db.KeyDeleteAsync(key);
                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error Remove value for redis");
                throw;
            }
        }

        public async Task<bool> IsInCacheAsync(string key)
        {
            try
            {
                var db = _redisClient.GetDatabase();
                return await db.KeyExistsAsync(key);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error Remove value for redis ");
                throw;
            }
        }

        public async Task<bool> SetIfNotExistAsync(string key, TimeSpan timeout)
        {
            try
            {
                IDatabase db = _redisClient.GetDatabase();
                return await db.StringSetAsync(key, 1, timeout, When.NotExists, CommandFlags.None);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Error SetIfNotExist for redis");
                return true;
            }
        }

    }
}
