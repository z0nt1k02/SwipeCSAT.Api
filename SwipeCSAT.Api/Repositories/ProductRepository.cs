using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;


namespace SwipeCSAT.Api.Repositories
{
    public class ProductRepository
    {
        private readonly SwipeCSATDbContext _context;
        

        public ProductRepository(SwipeCSATDbContext context, CategoryRepository category, ILogger<ProductRepository> loger)
        {
            _context = context;
        }

        public async Task<List<ProductEntity>> GetAllProducts()
        {
            return await _context.Products.AsNoTracking().Include(x => x.Category).Include(x => x.Criterions).AsNoTracking().ToListAsync();
        }

        public async Task<ProductEntity> GetProductByName(string name)
        {
            return await _context.Products.AsNoTracking().Include(x=>x.Criterions)
                .Include(x=>x.Category)
                .Include(x=>x.Reviews)
                .FirstOrDefaultAsync(x => x.Name == name)
                ?? throw new Exception("Данный продукт не найден");
        }
        public async Task<List<ProductEntity>> GetProductsWithCategory(string categoryName)
        {
            return await _context.Products.Include(categoryName => categoryName.Category)
                .Where(x => x.Category!.Name == categoryName)
                .Include(x=>x.Criterions).ToListAsync();
                
        }
        public async Task<ProductEntity> AddProduct(string name,string CategoryName,string description)
        {
            var category = await _context.Categories.Include(x=>x.Criterions).FirstOrDefaultAsync(x => x.Name == CategoryName)
                ?? throw new Exception("Данный продукт не найден");
            var product = new ProductEntity
            {
                Name = name,
                Id = Guid.NewGuid(),
                Description = description,
                Category = category,
                Criterions = category.Criterions

            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(string name)
        {
            var product = await _context.Products.Include(x=>x.Reviews).ThenInclude(x=>x.CriterionRatings).FirstOrDefaultAsync(x => x.Name == name)
                ?? throw new Exception("Данный продукт не найден");
            _context.Reviews.RemoveRange(product.Reviews);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
