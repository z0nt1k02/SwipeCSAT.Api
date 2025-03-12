
using SwipeCSAT.Api.Endpoints;
using SwipeCSAT.Api;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Repositories;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<SwipeCSATDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<CategoryRepository>();
var app = builder.Build();


app.Run();
