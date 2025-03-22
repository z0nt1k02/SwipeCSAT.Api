
using SwipeCSAT.Api.Endpoints;
using SwipeCSAT.Api;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using SwipeCSAT.Api.Interfaces;
using SwipeCSAT.Api.Infrastructure;
using SwipeCSAT.Api.Services;
using SwipeCSAT.Api.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.CookiePolicy;



var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentication(builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});

builder.Services.AddDbContext<SwipeCSATDbContext>(options =>
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

app.UseCookiePolicy(new CookiePolicyOptions{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});
app.UseAuthentication();
app.UseAuthorization();
app.AddMappedEndpoints();
//app.MapCriterionsEdnpoints();
////app.MapCategoriesEndpoints();
//app.MapProductsEndpoints();
//app.MapReviewsEndpoints();
//app.MapCriterionRatingEndpoints();
//app.MapUsersEndpoints();


app.Run();
