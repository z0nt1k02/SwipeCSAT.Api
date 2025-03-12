using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Repositories;

public class CategoryRepository
{
    public readonly SwipeCSATDbContext _context;
    
    public CategoryRepository(SwipeCSATDbContext context)
    {
        _context = context;
    }

    //Получение всех категорий
    public async Task<List<CategoryEntity>> GetAllCategories()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    //Получение категорий с продуктами  
    public async Task<List<CategoryEntity>> GetWithProducts(Guid id)
    {
        return await _context.Categories
            .Include(x => x.Products)
            .AsNoTracking()
            .ToListAsync();
    }

    //Получение категории по id
    public async Task<CategoryEntity?> GetCategoryById(Guid id)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    //Создание категории
    public async Task<CategoryEntity> CreateCategory(string Name)
    {
        var category = new CategoryEntity
        {
            Name = Name,
            Id = Guid.NewGuid(),
            Products = new List<ProductEntity>()
        };

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    //Обновление имени категории
    public async Task UpdateCategoryByName(string Name,string NewName)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == Name) 
            ?? throw new Exception("Категория не найдена");
        
        category.Name = NewName;
        await _context.SaveChangesAsync();
    }

    //Получение категории по имени
    public async Task<CategoryEntity> GetCategoryByName(string Name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == Name)
            ?? throw new Exception("Категория не найдена");

        return category;
    }
    //Удаление категории
    public async Task DeleteCategory(string name)
    {
        await _context.Categories
            .Where(x => x.Name == name)
            .ExecuteDeleteAsync();
    }

}

