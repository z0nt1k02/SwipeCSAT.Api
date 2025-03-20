
using SwipeCSAT.Api.Endpoints;
using SwipeCSAT.Api;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

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





var app = builder.Build();


app.MapCriterionsEdnpoints();
app.MapCategoriesEndpoints();
app.MapProductsEndpoints();
app.MapReviewsEndpoints();
app.MapCriterionRatingEndpoints();


app.Run();
