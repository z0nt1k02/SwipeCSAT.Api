using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Repositories;

public class CategoryRepository
{
    private readonly SwipeCSATDbContext _context;
    

    
    public CategoryRepository(SwipeCSATDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryEntity>> GetAllCategories()
    {
       return await _context.Categories.Include(c=>c.Criterions).Include(x=>x.Products).ToListAsync();
    }

    public async Task<CategoryEntity> GetCategoryByName(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Name == name)
            ?? throw new Exception("Данная категория не найдена");
    }

    public async Task<FullCategoryDto> Add(string name, List<string> criterionsNames)
    {
        var category = new CategoryEntity
        {
            Name = name,
            Id = Guid.NewGuid(),
            Products = new List<ProductEntity>(),
            Criterions = new List<CriterionEntity>()
        };
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        foreach (var criterionName in criterionsNames)
        {
            var criterion = await _context.Criterions.FirstOrDefaultAsync(x => x.Name == criterionName)
                ?? throw new Exception("Данный критерий не найден");

            category.Criterions.Add(criterion);
            criterion.Categories.Add(category);
            //_context.Criterions.Attach(criterion);
            
        }
        //_context.Categories.Attach(category);
        
        await _context.SaveChangesAsync();
        return category.ToDto();
    }

    public async Task DeleteCategory(string name)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == name)
            ?? throw new Exception("Данная категория не найдена");
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }



}

