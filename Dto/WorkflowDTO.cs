using CapitalApi.Models;

public class WorkflowDTO
{
    public Guid ProgramId { get; set; }
    public WorkflowStage Stage { get; set; }
    public List<ProgramApplicationDto>? Applications { get; set; }
}

public class ProgramApplicationDto
{
    public Guid ApplicationId { get; set; }
    public string? ApplicantName { get; set; }
    public DateTime ApplicationDate { get; set; }
}
