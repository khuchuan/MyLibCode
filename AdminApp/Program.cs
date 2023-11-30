using AdminApp.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using System;
using IdentityModel.Client;
using Blazored.LocalStorage;
using Helper;
using AdminApp.Services;
using AdminApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var services = builder.Services;

services.AddLogging();
services.AddMemoryCache();
services.AddBlazoredLocalStorage();
services.AddSingleton<ITokenProvider>(sp => new RedisProvider(builder.Configuration["DataProtectRedisDB"]));
services.AddSingleton<IRefreshTokenRedis, RefreshTokenRedis>();
//services.AddAutoMapper(typeof(MapperProfile).Assembly);
services.AddHttpContextAccessor();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddScoped<IAppStateService, AppStateService>();
services.AddScoped<BrowserService>();
services.AddAntDesign();
services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri)
});
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
services.AddScoped<SignInManager<IdentityUser>, ApplicationSignInManager>();
//services.AddReportServiceClient(Configuration["ReportServiceUrl"]);
//services.AddAdminServiceClient(Configuration["AdminServiceUrl"]);

var provider = services.BuildServiceProvider();
var refreshTokenRedis = provider.GetService<IRefreshTokenRedis>();





services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
              options.SlidingExpiration = true;
              options.LoginPath = "/Identity/Account/Login";
              options.AccessDeniedPath = "/Error/403";
              options.Cookie.Domain = builder.Configuration["Authen:DomainCookie"];
              options.Events = new CookieAuthenticationEvents
              {
                  OnValidatePrincipal = async x =>
                  {
                      var now = DateTimeOffset.UtcNow;
                      var timeElapsed = now.Subtract(x.Properties.IssuedUtc.Value);
                      var timeRemaining = x.Properties.ExpiresUtc.Value.Subtract(now);
                      if (timeElapsed > timeRemaining)
                      {
                          var refreshToken = x.HttpContext?.Request.Cookies["refresh_token"];
                          if (refreshToken != null)
                          {
                              var response = await new HttpClient().RequestRefreshTokenAsync(new RefreshTokenRequest
                              {
                                  Address = builder.Configuration["Authen:Url"],
                                  RefreshToken = refreshToken,
                                  ClientId = builder.Configuration["Authen:AudienceId"]
                              });
                              if (!response.IsError)
                              {
                                  var session = x.Principal?.Claims.Where(x => x.Type == "sessionId").Select(c => c.Value).SingleOrDefault();
                                  if (!string.IsNullOrEmpty(session))
                                  {
                                      //await refreshTokenRedis.RefreshInRedis(session, response.AccessToken, TimeSpan.FromSeconds(response.ExpiresIn));
                                  }
                              }
                          }
                      }
                  }
              };
          });


services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
