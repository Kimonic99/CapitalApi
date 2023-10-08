 using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace CapitalApi.Models;

public class Template
{
    public Guid Id { get; set; } // Unique template identifier

    public Guid ProgramId { get; set; } // Reference to the associated program
    public string? CoverImageUrl { get; set; }
    public PersonalInfo? PersonalInfo { get; set; }
    public Profile? Profile { get; set; }
    public List<AdditionalQuestion>? AdditionalQuestions { get; set; }
    public string DocumentType { get; } = "Template"; // To identify the document type in Cosmos DB
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

public class PersonalInfo
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

public class Profile
{
    public string? Education { get; set; }
    public string? Experience { get; set; }
    public string? ResumeUrl { get; set; }
}

public class AdditionalQuestion
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


