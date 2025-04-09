using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using SwipeCSAT.Api;
using SwipeCSAT.Api.Authorization;
using SwipeCSAT.Api.Extensions;
using SwipeCSAT.Api.Infrastructure;
using SwipeCSAT.Api.Interfaces;
using SwipeCSAT.Api.Repositories;
using SwipeCSAT.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<SwipeCSAT.Api.Repositories.AuthorizationOptions>(configuration.GetSection(nameof(SwipeCSAT.Api.Repositories.AuthorizationOptions)));


services.AddApiAuthentication(configuration);


services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

services.AddDbContext<SwipeCsatDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
});

services.AddExceptionHandler(options => 
{
    options.ExceptionHandlingPath = "/error";
});


builder.Logging.AddConsole();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CriterionsRepository>();
builder.Services.AddScoped<ReviewsRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();



var app = builder.Build();



app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    // Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();
app.UseExceptionHandler();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.AddMappedEndpoints();


app.Run();
public partial class Program { }