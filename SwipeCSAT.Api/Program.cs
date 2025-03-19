
using SwipeCSAT.Api.Endpoints;
using SwipeCSAT.Api;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Repositories;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<SwipeCSATDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
});
builder.Logging.AddConsole();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CriterionsRepository>();
builder.Services.AddScoped<ReviewsRepository>();

//builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();


app.MapCriterionsEdnpoints();
app.MapCategoriesEndpoints();
app.MapProductsEndpoints();
app.MapReviewsEndpoints();
app.MapCriterionRatingEndpoints();


app.Run();
