using YarpGateway;

var builder = WebApplication.CreateBuilder(args);

builder.AddFileConfigs();


builder.Services.AddCors(options =>
{
    // TODO: Read allowed origins from configuration
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
    options.AddPolicy("CorsPolicyAllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

// Add reverse proxy configuration (YARP)
builder.Services.AddReverseProxy(builder.Configuration);

var app = builder.Build();

app.UseCors();
app.UseHttpLogging();
app.UseRouting();

app.MapReverseProxy();

app.Run();

