using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static List<CategoryDto> Categories = new List<CategoryDto>
        {
            new CategoryDto(1,"Рестораны",new List<string>{"Кухня","Атмосфера","Цена"}),
            new CategoryDto(2,"Смартфоны",new List<string>{"Автономность","Камера","Производительность"})
            
        };
          

        public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/categories");

            group.MapGet("/", () =>
            {
                return Results.Ok(Categories);
            });

            group.MapPost("/", (CreateCategoryDto createCategoryDto) =>
            {
                CategoryDto NewCategory = createCategoryDto.ToDto();
                Categories.Add(NewCategory);

                return Results.Created($"/api/categories/{NewCategory.Id}",NewCategory);
            });

            
            


            return group;
        }
    }
}
