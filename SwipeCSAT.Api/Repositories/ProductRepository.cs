using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;
using Microsoft.AspNetCore.JsonPatch;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Mapping;


namespace SwipeCSAT.Api.Repositories
{
    public class ProductRepository
    {

        public readonly SwipeCSATDbContext _context;
        public readonly CategoryRepository categoryRepository;

        public ProductRepository(SwipeCSATDbContext context,CategoryRepository category)
        {
            _context = context;
            categoryRepository = category;


        }

        //Получение всех продуктов
        public async Task<List<ProductEntity>> GetAllProducts()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        //Получение продуктов с определенной категорией  
        public async Task<List<ProductEntity>> GetCategoryProducts(string CategoryName)
        {
            return await _context.Products
                .Include(x=> x.Category)
                .Where(x => x.Category!.Name == CategoryName)
                .AsNoTracking()
                .ToListAsync();
            
        }

        

        
    }
}
