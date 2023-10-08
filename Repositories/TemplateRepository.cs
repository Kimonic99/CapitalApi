using CapitalApi.Cosmos;
using CapitalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CapitalApi.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly CosmosDbContext _dbContext;

    public TemplateRepository(CosmosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Template?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Templates
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Template>> GetAllAsync()
    {
        return await _dbContext.Templates.ToListAsync();
    }

    public async Task AddAsync(Template template)
    {
        await _dbContext.Templates.AddAsync(template);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Template template)
    {
        _dbContext.Entry(template).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var template = await _dbContext.Templates
                          .FirstOrDefaultAsync(p => 
                              p.Id == id)
                      ?? throw new Exception("Not found");
        
        _dbContext.Templates.Remove(template);
        await _dbContext.SaveChangesAsync();
        
    }
}

public interface ITemplateRepository
{
    Task<Template?> GetByIdAsync(Guid id);
    Task<IEnumerable<Template>> GetAllAsync();
    Task AddAsync(Template template);
    Task UpdateAsync(Template template);
    Task DeleteAsync(Guid id);
}
