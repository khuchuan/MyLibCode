using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HttpRequestException, 5XX and 408  
var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(3);

var retryPolicyAdvanced = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));


var services = builder.Services;
services.AddHttpClient(
              "InventoryService",
              client =>
              {
                  client.BaseAddress = new Uri("http://localhost:5213/"); // PollyServerApi
                  client.DefaultRequestHeaders.Add("Accept", "application/json");
              })
    .AddPolicyHandler(retryPolicy);
    //.AddPolicyHandler(retryPolicyAdvanced);

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
