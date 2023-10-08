using CapitalApi.Cosmos;
using Microsoft.EntityFrameworkCore;
using CapitalApi.Models;

namespace CapitalApi.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly CosmosDbContext _dbContext;

        public WorkflowRepository(CosmosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkflowDTO>> GetWorkflowsForStagesAsync()
{
    var stagesToRetrieve = new List<WorkflowStage>
    {
        WorkflowStage.Shortlisted,
        WorkflowStage.VideoInterview,
        WorkflowStage.Placement
    };

    var workflows = await _dbContext.Workflows
        .Where(w => stagesToRetrieve.Contains(w.Stage))
        .Include(w => w.Applications)
        .ToListAsync(); // Materialize the query into a list

    return workflows.Select(w => new WorkflowDTO
    {
        ProgramId = w.ProgramId,
        Stage = w.Stage,
        Applications = w.Applications?.Select(a => new ProgramApplicationDto
        {
            ApplicationId = a.ApplicationId,
            ApplicantName = a.ApplicantName,
            ApplicationDate = a.ApplicationDate
        }).ToList() ?? new List<ProgramApplicationDto>()
    }).ToList();
}


        public async Task<bool> UpdateWorkflowStageAsync(Guid workflowId, WorkflowStage newStage)
        {
            var workflow = await _dbContext.Workflows.FindAsync(workflowId);
            if (workflow == null)
            {
                return false; // Workflow not found
            }

            workflow.Stage = newStage;
            workflow.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}


public interface IWorkflowRepository
    {
        Task<List<WorkflowDTO>> GetWorkflowsForStagesAsync();
        Task<bool> UpdateWorkflowStageAsync(Guid workflowId, WorkflowStage newStage);
    }
