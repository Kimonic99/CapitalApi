using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CapitalApi.Cosmos;
using CapitalApi.Dto;
using CapitalApi.Models;

namespace CapitalApi.Repositories
{
    public class PreviewRepository : IPreviewRepository
    {
        private readonly CosmosDbContext _dbContext;
        private readonly IMapper _mapper;

        public PreviewRepository(CosmosDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PreviewDTO?> GetPreviewDataAsync()
        {
            // Retrieve data from Program, Template, and Workflow entities
            var programData = await _dbContext.ProgramModels.FirstOrDefaultAsync();
            var templatesData = await _dbContext.Templates.ToListAsync();
            var workflowData = await _dbContext.Workflows.FirstOrDefaultAsync();

            if (programData == null || workflowData == null)
            {
                // Handle the case where no data is found, return null or throw an exception as needed.
                return null;
            }

            // Map the retrieved data to the PreviewDto
            var previewDto = new PreviewDTO
            {
                Program = _mapper.Map<ProgramDTO>(programData),
                Templates = _mapper.Map<List<TemplateDTO>>(templatesData),
                Workflow = _mapper.Map<WorkflowDTO>(workflowData)
            };

            return previewDto;
        }


    }
}
public interface IPreviewRepository
{
    Task<PreviewDTO?> GetPreviewDataAsync();
}