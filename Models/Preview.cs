namespace CapitalApi.Models
{
    public class Preview
    {
        public Guid Id { get; set; }
        public Program? Program { get; set; }
        public List<Template>? Templates { get; set; }
        public Workflow? Workflow { get; set; }
    }

    public class Program
    {
        public Guid Id { get; set; }
        public string? ProgramTitle { get; set; }
        public string? Summary { get; set; }
        public string? ProgramDescription { get; set; }
        public string? SkillsRequired { get; set; }
        public string? ProgramBenefits { get; set; }
        public string? ApplicationCriteria { get; set; }
        public ProgramAdditionalInfo? AdditionalInfo { get; set; }
    }

    public class ProgramAdditionalInfo
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

    
}
