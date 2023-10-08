using System.ComponentModel.DataAnnotations;
using CapitalApi.Models;

namespace CapitalApi.Dto;
public class ProgramDTO
{
    public Guid Id { get; set; }
    public string? ProgramTitle { get; set; }
    public string? Summary { get; set; }
    public string? ProgramDescription { get; set; }
    public string? SkillsRequired { get; set; }
    public string? ProgramBenefits { get; set; }
    public string? ApplicationCriteria { get; set; }

    public ProgramAdditionalInfoModel? ProgramAdditionalInfo { get; set; }

    public ProgramAdditionalInfoDTO? AdditionalInfo { get; set; }
}

public class ProgramAdditionalInfoDTO
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
