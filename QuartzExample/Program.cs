using Quartz;
using QuartzExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Quartz services
builder.Services.AddQuartz(q => {
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("KeepSessionJob");
    q.AddJob<KeepSessionJob>(j => j.WithIdentity(jobKey));

    q.AddTrigger(cfg => cfg.ForJob(jobKey)
    .WithIdentity("KeepSessionJob-trigger")
    .WithCronSchedule("0/20 * * * * ?")); // Run every 20 seconds                                              
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
