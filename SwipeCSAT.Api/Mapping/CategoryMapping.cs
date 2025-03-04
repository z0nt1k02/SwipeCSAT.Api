using SwipeCSAT.Api.Dtos;

namespace SwipeCSAT.Api.Mapping
{
    public static class CategoryMapping
    {

        //Преобразование категории в DTO
        public static CategoryDto ToDto(this CreateCategoryDto createCategoryDto)
        {
            CategoryDto dto = new CategoryDto(
                createCategoryDto.Id,
                createCategoryDto.Name,
                createCategoryDto.Properties
            );
            return dto;
        }

        //Создание словаря из списка ключей
        //private static Dictionary<string,int>CreateKeysInProperties(CreateCategoryDto createCategoryDto)
        //{
        //    Dictionary<string, int> properties = new Dictionary<string, int>(); 
        //    foreach (var key in createCategoryDto.Properties)
        //    {
        //       properties.Add(key, 0);
        //    }
        //    return properties;
        //}
    }
}
