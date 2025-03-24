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
services.Configure<AuthorizationOptions>(configuration.GetSection(nameof(AuthorizationOptions)));

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

app.MapGet("/", async (HttpContext context, CategoryRepository repository) =>
{
    if (!context.User.HasClaim("Admin", "true"))
    {
        return Results.Unauthorized();
    }

    return Results.Ok("Вы авторизованы как admin");
}).RequireAuthorization(policy => policy.AddRequirements(new PermissionRequirment([Permission.Read])));

app.Run();