using QuartzDemo;
using Quartz;
using Microsoft.Extensions.Hosting;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Add the required Quartz.NET services
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            q.AddJobAndTrigger<HelloWorldJob>(hostContext.Configuration);
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    });


host.Run();
