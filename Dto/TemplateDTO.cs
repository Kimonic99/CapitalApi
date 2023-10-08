using System.ComponentModel.DataAnnotations;

namespace CapitalApi.Dto;
public class TemplateDTO
{
    public Guid ProgramId { get; set; }
    public string? CoverImageBase64 { get; set; }
    public PersonalInfoDTO? PersonalInfo { get; set; }
    public ProfileDTO? Profile { get; set; }
    public List<AdditionalQuestionDTO>? AdditionalQuestions { get; set; }
}

public class PersonalInfoDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Nationality { get; set; }
    public string? CurrentResidence { get; set; }
    public string? IDNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Gender { get; set; }
}

public class ProfileDTO
{
    public string? Education { get; set; }
    public string? Experience { get; set; }
    public string? ResumeBase64 { get; set; }
}

public class AdditionalQuestionDTO
{
    public string? Question { get; set; }
    public QuestionType Type { get; set; }
    public List<string>? Options { get; set; } // For Dropdown and MultipleChoice types
}

public enum QuestionType
{
    Paragraph,
    ShortAnswer,
    YesNo,
    Dropdown,
    MultipleChoice,
    Date,
    Number,
    FileUpload
}
