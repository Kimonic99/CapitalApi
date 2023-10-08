using Microsoft.EntityFrameworkCore;
using CapitalApi.Models;

namespace CapitalApi.Cosmos;
public class CosmosDbContext : DbContext
{
    public DbSet<ProgramModel> ProgramModels => Set<ProgramModel>();
    public DbSet<Template> Templates => Set<Template>();
    public DbSet<Workflow> Workflows => Set<Workflow>();
    public DbSet<Preview> Previews => Set<Preview>();

    public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
    {}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgramModel>()
            .ToContainer("ProgramModels")
            .HasPartitionKey(p => p.Id);

        modelBuilder.Entity<Template>() 
            .ToContainer("Templates")
            .HasPartitionKey(p => p.Id);

        modelBuilder.Entity<Workflow>() 
            .ToContainer("Workflows")
            .HasPartitionKey(p => p.Id);

        modelBuilder.Entity<Preview>() 
            .ToContainer("Previews")
            .HasPartitionKey(p => p.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}