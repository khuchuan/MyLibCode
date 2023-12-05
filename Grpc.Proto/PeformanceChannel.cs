using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;

namespace GrpcLib.Proto
{
    public interface IPerformanceChannel
    {
        GrpcChannel GetChannel();
    }
    public interface ISalesChannel : IPerformanceChannel
    {
    }
    public sealed class PerformanceChannel : IPerformanceChannel, ISalesChannel, IDisposable
    {
        private readonly GrpcChannel channel;
        private bool disposed = false;
        private readonly object _lockObj = new object();
        public PerformanceChannel(string serverUrl, int maxRetry = 2)
        {
            if (!string.IsNullOrEmpty(serverUrl))
                channel = GrpcChannel.ForAddress(serverUrl, new GrpcChannelOptions
                {
                    HttpHandler = GetDefaultSocketsHttpHandler(),
                    ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } }
                });
        }
        public PerformanceChannel(string serverUrl, int? maxReceiveMessageSize, int? maxSendMessageSize, int maxRetry = 2)
        {
            if (!string.IsNullOrEmpty(serverUrl))
            {
                var option = new GrpcChannelOptions
                {
                    HttpHandler = GetDefaultSocketsHttpHandler(),
                    ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } }
                };

                if (maxReceiveMessageSize.HasValue && maxReceiveMessageSize > 0)
                    option.MaxReceiveMessageSize = maxReceiveMessageSize * 1024 * 1024;
                if (maxSendMessageSize.HasValue && maxSendMessageSize > 0)
                    option.MaxSendMessageSize = maxSendMessageSize * 1024 * 1024;

                channel = GrpcChannel.ForAddress(serverUrl, option);
            }
        }
        private static MethodConfig GetDefaultMethodConfig(int maxRetry)
        {
            return new MethodConfig
            {
                Names = { MethodName.Default },
                RetryPolicy = new RetryPolicy
                {
                    MaxAttempts = maxRetry,
                    InitialBackoff = TimeSpan.FromSeconds(1),
                    MaxBackoff = TimeSpan.FromSeconds(5),
                    BackoffMultiplier = 1.5,
                    RetryableStatusCodes = { StatusCode.Unavailable }
                }
            };
        }
        private static SocketsHttpHandler GetDefaultSocketsHttpHandler()
        {
            return new SocketsHttpHandler
            {
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true
            };
        }

        public GrpcChannel GetChannel()
        {

            if (channel.State == ConnectivityState.TransientFailure)
            {
                lock (_lockObj)
                {
                    channel.ConnectAsync().ConfigureAwait(true);
                }
            }
            return channel;
        }
        public void Dispose()
        {
            if (disposed)
                return;
            channel.Dispose();
            GC.SuppressFinalize(this);
            disposed = true;
        }
        ~PerformanceChannel()
        {
            Dispose();
        }
    }
}
