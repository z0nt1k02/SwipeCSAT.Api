using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Repositories;

public class CriterionsRepository
{
    private readonly SwipeCsatDbContext _context;

    public CriterionsRepository(SwipeCsatDbContext context)
    {
        _context = context;
    }

    public async Task<List<CriterionEntity>> GetAllCriterions()
    {
        return await _context.Criterions.Include(x => x.Categories).ToListAsync();
    }

    public async Task<CriterionEntity> GetCriterionByName(string name)
    {
        return await _context.Criterions.FirstOrDefaultAsync(x => x.Name == name)
               ?? throw new Exception("Данный критерий не найден");
    }

    public async Task<CriterionEntity> AddCriterion(string name)
    {
        var criterion = new CriterionEntity
        {
            Name = name,
            Id = Guid.NewGuid()
        };
        _context.Criterions.Add(criterion);
        await _context.SaveChangesAsync();
        return criterion;
    }

    public async Task DeleteCriterion(string name)
    {
        var criterion = await _context.Criterions.FirstOrDefaultAsync(x => x.Name == name)
                        ?? throw new Exception("Данный критерий не найден");
        _context.Criterions.Remove(criterion);
        await _context.SaveChangesAsync();
    }
}