using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Endpoints;

namespace SwipeCSAT.Api.Mapping
{
    public static class ProductsMapping
    {
        public static ProductDto ToDto(this CreateProductDto createProductDto)
        {
            ProductDto productDto = new ProductDto
            (
                createProductDto.Id,
                createProductDto.Name,
                CreateDictionary(GetProperties(createProductDto)),
                createProductDto.CategoryId
            );
            return productDto;
        }
        private static List<string> GetProperties(CreateProductDto createProduct)
        {
            try
            {
                var CategoryId = createProduct.CategoryId;
                CategoryDto? Category = CategoriesEndpoints.Categories.Find(x => x.Id == CategoryId);
                if (Category == null)
                {
                    throw new Exception($"Category with Id {CategoryId} not found.");
                }
                return Category.Properties;
            }
            catch (Exception ex)
            {
                // Логирование ошибки или выполнение других действий
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static Dictionary<string,int> CreateDictionary(List<string> properties)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (var property in properties)
            {
                dictionary.Add(property, 0);
            }
            return dictionary;
        }
    }
}
