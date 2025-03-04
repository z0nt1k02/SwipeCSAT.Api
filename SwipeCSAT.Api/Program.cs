using SwipeCSAT.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

app.Run();
