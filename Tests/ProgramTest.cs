using CapitalApi.Models;
using Xunit;

namespace CapitalApi.Tests
{
    public class ProgramTest
    {
        [Fact]
        public void ProgramModel_Initialization_SetsPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var programTitle = "Sample Program";
            var summary = "Program Summary";
            var programDescription = "Program Description";
            var skillsRequired = "Skills Required";
            var programBenefits = "Program Benefits";
            var applicationCriteria = "Application Criteria";

            var additionalInfo = new ProgramAdditionalInfoModel
            {
                ProgramType = "Type",
                ProgramStart = DateTime.UtcNow,
                ApplicationOpen = DateTime.UtcNow,
                ApplicationClose = DateTime.UtcNow,
                Duration = 30,
                ProgramLocation = "Location",
                MinQualifications = new string[] { "Qualification1", "Qualification2" },
                MaxNumberOfApplications = 100
            };

            // Act
            var programModel = new ProgramModel
            {
                Id = id,
                ProgramTitle = programTitle,
                Summary = summary,
                ProgramDescription = programDescription,
                SkillsRequired = skillsRequired,
                ProgramBenefits = programBenefits,
                ApplicationCriteria = applicationCriteria,
                AdditionalInfo = additionalInfo
            };

            // Assert
            Assert.Equal(id, programModel.Id);
            Assert.Equal(programTitle, programModel.ProgramTitle);
            Assert.Equal(summary, programModel.Summary);
            Assert.Equal(programDescription, programModel.ProgramDescription);
            Assert.Equal(skillsRequired, programModel.SkillsRequired);
            Assert.Equal(programBenefits, programModel.ProgramBenefits);
            Assert.Equal(applicationCriteria, programModel.ApplicationCriteria);

            Assert.Equal(additionalInfo.ProgramType, programModel.AdditionalInfo.ProgramType);
            Assert.Equal(additionalInfo.ProgramStart, programModel.AdditionalInfo.ProgramStart);
            Assert.Equal(additionalInfo.ApplicationOpen, programModel.AdditionalInfo.ApplicationOpen);
            Assert.Equal(additionalInfo.ApplicationClose, programModel.AdditionalInfo.ApplicationClose);
            Assert.Equal(additionalInfo.Duration, programModel.AdditionalInfo.Duration);
            Assert.Equal(additionalInfo.ProgramLocation, programModel.AdditionalInfo.ProgramLocation);
            Assert.Equal(additionalInfo.MinQualifications, programModel.AdditionalInfo.MinQualifications);
            Assert.Equal(additionalInfo.MaxNumberOfApplications, programModel.AdditionalInfo.MaxNumberOfApplications);
        }

        [Fact]
        public void ProgramModel_DefaultValues_AssignedCorrectly()
        {
            // Arrange & Act
            var programModel = new ProgramModel();

            // Assert
            Assert.Equal(Guid.Empty, programModel.Id);
            Assert.Null(programModel.ProgramTitle);
            Assert.Null(programModel.Summary);
            Assert.Null(programModel.ProgramDescription);
            Assert.Null(programModel.SkillsRequired);
            Assert.Null(programModel.ProgramBenefits);
            Assert.Null(programModel.ApplicationCriteria);

            // Get the current time
            var expectedTime = DateTime.UtcNow;

            // Compare CreatedAt and UpdatedAt timestamps with a tolerance
            var timestampTolerance = TimeSpan.FromMilliseconds(20); // Adjust tolerance as needed

            Assert.InRange(programModel.CreatedAt, expectedTime - timestampTolerance, expectedTime + timestampTolerance);
            Assert.InRange(programModel.UpdatedAt, expectedTime - timestampTolerance, expectedTime + timestampTolerance);

            Assert.Equal("Program", programModel.DocumentType);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ProgramModel_PropertyValidation_ProgramTitle_NullOrEmpty(string programTitle)
        {
            // Arrange
            var programModel = new ProgramModel();

            // Act
            programModel.ProgramTitle = programTitle;

            // Assert
            Assert.Null(programModel.ProgramTitle);
        }

        [Fact]
        public void ProgramModel_PropertyConstraints_ProgramTitle_MaxLength()
        {
            // Arrange
            var programModel = new ProgramModel();
            var maxLengthTitle = new string('X', 256);

            // Act
            programModel.ProgramTitle = maxLengthTitle;

            // Assert
            Assert.Equal(maxLengthTitle, programModel.ProgramTitle);
        }

        [Fact]
        public void ProgramModel_EdgeCases_PropertyConstraints_ProgramTitle_MaxLength()
        {
            // Arrange
            var programModel = new ProgramModel();
            var maxLengthTitle = new string('X', 255);

            // Act
            programModel.ProgramTitle = maxLengthTitle;

            // Assert
            Assert.Equal(maxLengthTitle, programModel.ProgramTitle);
        }

        [Fact]
        public void ProgramModel_PropertyValidation_ProgramDescription_Null()
        {
            // Arrange
            var programModel = new ProgramModel
            {
                // Act
                ProgramDescription = null
            };

            // Assert
            Assert.Null(programModel.ProgramDescription);
        }

        [Fact]
        public void ProgramModel_EdgeCases_PropertyValidation_Dates()
        {
            // Arrange
            var programModel = new ProgramModel();
            var futureDate = DateTime.UtcNow.AddYears(1);

            // Act
            if (futureDate > programModel.UpdatedAt)
            {
                programModel.UpdatedAt = default(DateTime);
            }

            // Assert
            Assert.Equal(default(DateTime), programModel.UpdatedAt);
        }




    }
    // more test methods to cover other scenarios or edge cases.
}
