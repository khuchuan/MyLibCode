using DTO;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Helper
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(120);
        private readonly ILogger<HttpClientHelper> _logger;
        public HttpClientHelper(ILogger<HttpClientHelper> logger, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<T?> PostAsync<T>(string url, string requestData, Dictionary<string, string>? headers = null, TimeSpan? timeout = null, string? traceId = null)
        {
            var result = default(T);
            try
            {
                var request = new StringContent(requestData, Encoding.UTF8, "application/json");
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
                if (!string.IsNullOrEmpty(traceId))
                {
                    request.Headers.Add("traceid", traceId);
                }
                using var client = _clientFactory.CreateClient();
                client.Timeout = timeout == null ? _defaultTimeout : timeout.Value;
                using var response = await client.PostAsync(url, request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    try
                    {
                        result = await JsonSerializer.DeserializeAsync<T>(responseStream);
                    }
                    catch (Exception ex)
                    {
                        using StreamReader reader = new(responseStream);
                        string text = await reader.ReadToEndAsync();
                        _logger.LogError(ex, "PostAsync Deserialize Error: {Response}", text);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PostAsync Exception: ");
                throw;
            }
            return result;
        }

        public async Task<T?> PostAsync<T>(string url, FormUrlEncodedContent? content = null, TimeSpan? timeout = null, string? traceId = null)
        {
            var result = default(T);
            try
            {
                using var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.Timeout = timeout == null ? _defaultTimeout : timeout.Value;
                if (!string.IsNullOrEmpty(traceId))
                {
                    client.DefaultRequestHeaders.Add("traceid", traceId);
                }
                using var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    try
                    {
                        result = await JsonSerializer.DeserializeAsync<T>(responseStream);
                    }
                    catch (Exception ex)
                    {
                        using StreamReader reader = new(responseStream);
                        string text = await reader.ReadToEndAsync();
                        _logger.LogError(ex, "PostAsync Deserialize Error: {Response}", text);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PostAsync Exception: ");
                throw;
            }
            return result;
        }

        public async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null, TimeSpan? timeout = null, string? traceId = null)
        {
            var result = default(T);
            try
            {
                using var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = timeout == null ? _defaultTimeout : timeout.Value;
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                if (!string.IsNullOrEmpty(traceId))
                {
                    client.DefaultRequestHeaders.Add("traceid", traceId);
                }
                using var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    try
                    {
                        result = await JsonSerializer.DeserializeAsync<T>(responseStream);
                    }
                    catch (Exception ex)
                    {
                        using StreamReader reader = new(responseStream);
                        string text = await reader.ReadToEndAsync();
                        _logger.LogError(ex, "GetAsync Deserialize Error: {Response}", text);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAsync Exception: ");
                throw;
            }
            return result;
        }
        public async Task<string?> GetTokenAsync(string url, string username, string password, string audienceClientId, string? traceId = null)
        {
            try
            {
                using var content = new FormUrlEncodedContent(new[]
                   {
                        new KeyValuePair<string, string>("username",username),
                        new KeyValuePair<string, string>("password",password),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("device_id", Environment.MachineName),
                        new KeyValuePair<string, string>("client_id", audienceClientId),
                    });
                var response = await PostAsync<TokenResponse>(url, content, TimeSpan.FromSeconds(60), traceId);
                return response?.access_token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetTokenAsync");
                return string.Empty;
            }
        }
    }

    public interface IHttpClientHelper
    {
        Task<T?> PostAsync<T>(string url, string requestData, Dictionary<string, string>? headers = null, TimeSpan? timeout = null, string? traceId = null);
        Task<T?> PostAsync<T>(string url, FormUrlEncodedContent? content = null, TimeSpan? timeout = null, string? traceId = null);
        Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null, TimeSpan? timeout = null, string? traceId = null);
        Task<string?> GetTokenAsync(string url, string username, string password, string audienceClientId, string? traceId = null);
    }
}
