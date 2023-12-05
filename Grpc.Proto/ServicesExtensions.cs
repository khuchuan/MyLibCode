using AuditLib.Grpc;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.ClientFactory;
using GrpcLib.Proto;
using Microsoft.Extensions.DependencyInjection;
using ReportService;
using SalesService;

namespace Grpc.Proto
{
    public static class ServicesExtensions
    {
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

        public static IServiceCollection AddSalesClient(this IServiceCollection serviceCollection, string address, int maxRetry = 2)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddTransient<ForwardHeaderAppInterceptor>();
            serviceCollection.AddSingleton<ISalesChannel>(x => new PerformanceChannel(address));
            return serviceCollection;
        }
        public static IServiceCollection AddSalesServiceClient(this IServiceCollection serviceCollection, string address, int maxRetry = 2)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddTransient<ForwardHeaderInterceptor>();
            serviceCollection.AddGrpcClient<Config.ConfigClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                };
            }).ConfigureChannel(o =>
            {
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<ForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<Sales.SalesClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<ForwardHeaderInterceptor>(InterceptorScope.Client);


            return serviceCollection;
        }

        public static IServiceCollection AddReportServiceClient(this IServiceCollection serviceCollection, string address, int maxRetry = 2)
        {
            serviceCollection.AddScoped<CMSForwardHeaderInterceptor>();
            serviceCollection.AddGrpcClient<Transaction.TransactionClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 15 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            })
            .AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);


            serviceCollection.AddGrpcClient<Summary.SummaryClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<AuditLog.AuditLogClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            })
            .AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<RequestSupport.RequestSupportClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            })
            .AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<ReportService.RefundOrder.RefundOrderClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 50 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            })
           .AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);


            //serviceCollection.AddGrpcClient<ReportService.PromotionCampaign.PromotionCampaignClient>(o =>
            //{
            //    o.Address = new Uri(address);
            //}).ConfigurePrimaryHttpMessageHandler(() =>
            //{
            //    return new SocketsHttpHandler
            //    {
            //        PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            //        EnableMultipleHttp2Connections = true,
            //        KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            //        KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
            //    };
            //}).ConfigureChannel(o =>
            //{
            //    o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            //})
            //.AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            return serviceCollection;
        }

        public static IServiceCollection AddAdminServiceClient(this IServiceCollection serviceCollection, string address, int maxRetry = 2)
        {
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddTransient<CMSForwardHeaderInterceptor>();
            serviceCollection.AddGrpcClient<AdminService.TransactionAdmin.TransactionAdminClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 15 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<AdminService.RequestSupportAdmin.RequestSupportAdminClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 50 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<AdminService.PaybillConfig.PaybillConfigClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 15 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<AdminService.PromotionCampaign.PromotionCampaignClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 15 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            serviceCollection.AddGrpcClient<AdminService.RefundOrderAdmin.RefundOrderAdminClient>(o =>
            {
                o.Address = new Uri(address);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30)
                };
            }).ConfigureChannel(o =>
            {
                o.MaxReceiveMessageSize = 50 * 1024 * 1024; // 15 MB
                o.ServiceConfig = new ServiceConfig { MethodConfigs = { GetDefaultMethodConfig(maxRetry) } };
            }).AddInterceptor<CMSForwardHeaderInterceptor>(InterceptorScope.Client);

            return serviceCollection;
        }
    }
}
