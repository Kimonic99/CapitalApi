using CapitalApi.Cosmos;
using CapitalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CapitalApi.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly CosmosDbContext _dbContext;

    public ProgramRepository(CosmosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProgramModel?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ProgramModels
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProgramModel>> GetAllAsync()
    {
        return await _dbContext.ProgramModels.ToListAsync();
    }

    public async Task AddAsync(ProgramModel programModel)
    {
        await _dbContext.ProgramModels.AddAsync(programModel);
        await _dbContext.SaveChangesAsync();
    }

public async Task UpdateAsync(ProgramModel programModel)
{
    var existingProgram = await _dbContext.ProgramModels.FirstOrDefaultAsync(p => p.Id == programModel.Id);
    if (existingProgram == null)
    {
        // Log the Id that was not found
        Console.WriteLine($"Program not found with Id: {programModel.Id}");
        throw new Exception("Program not found");
    }

    // Update the properties you want to change
    existingProgram.ProgramTitle = programModel.ProgramTitle;
    existingProgram.Summary = programModel.Summary;
    existingProgram.ProgramDescription = programModel.ProgramDescription;
    existingProgram.SkillsRequired = programModel.SkillsRequired;
    existingProgram.ProgramBenefits = programModel.ProgramBenefits;

    // Mark the entity as modified and save changes
    _dbContext.Entry(existingProgram).State = EntityState.Modified;
    await _dbContext.SaveChangesAsync();
}


    public async Task DeleteAsync(Guid id)
    {
        var programModel = await _dbContext.ProgramModels
                          .FirstOrDefaultAsync(p => 
                              p.Id == id)
                      ?? throw new Exception("Not found");
        
        _dbContext.ProgramModels.Remove(programModel);
        await _dbContext.SaveChangesAsync();
        
    }
}

public interface IProgramRepository
{
    Task<ProgramModel?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProgramModel>> GetAllAsync();
    Task AddAsync(ProgramModel programModel);
    Task UpdateAsync(ProgramModel programModel);
    Task DeleteAsync(Guid id);
}
