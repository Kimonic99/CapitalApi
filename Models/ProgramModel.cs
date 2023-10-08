namespace CapitalApi.Models;


public class ProgramModel
{
    public Guid Id { get; set; } // Unique program identifier
    public string? ProgramTitle { get; set; }
    public string? Summary { get; set; }
    public string? ProgramDescription { get; set; }
    public string? SkillsRequired { get; set; }
    public string? ProgramBenefits { get; set; }
    public string? ApplicationCriteria { get; set; }
    public ProgramAdditionalInfoModel? AdditionalInfo { get; set; }
    public string DocumentType { get; } = "Program"; // To identify the document type in Cosmos DB
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class ProgramAdditionalInfoModel
{
    public string? ProgramType { get; set; }
    public DateTime ProgramStart { get; set; }
    public DateTime ApplicationOpen { get; set; }
    public DateTime ApplicationClose { get; set; }
    public int Duration { get; set; }
    public string? ProgramLocation { get; set; }
    public string[]? MinQualifications { get; set; }
    public int MaxNumberOfApplications { get; set; }
}
