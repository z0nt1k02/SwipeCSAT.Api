using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using SwipeCSAT.Api;
using SwipeCSAT.Api.Authorization;
using SwipeCSAT.Api.Enums;
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


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddDbContext<SwipeCsatDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
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
app.UseAuthentication();
app.UseAuthorization();
app.AddMappedEndpoints();

app.MapGet("get", () =>
{
    return Results.Ok("ok");
}).RequirePermissions(Permission.Read);

app.Run();