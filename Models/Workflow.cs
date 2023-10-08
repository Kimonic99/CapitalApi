namespace CapitalApi.Models;

public class Workflow
{
    public Guid Id { get; set; } // Unique workflow identifier
    public Guid ProgramId { get; set; } // Reference to the associated program
    public WorkflowStage Stage { get; set; } // Represents the current stage
    public List<ProgramApplication>? Applications { get; set; } // List of program applications for this stage
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class ProgramApplication
{
    public Guid ApplicationId { get; set; } // Unique application identifier
    public string? ApplicantName { get; set; } // Name of the applicant
    public DateTime ApplicationDate { get; set; } // Date of application
}

public enum WorkflowStage
{
    Applied,
    Shortlisted,
    VideoInterview,
    FirstRoundZoomInterview,
    InPersonMeeting,
    Placement,
    Offered
}