using CapitalApi.Cosmos;
using CapitalApi.Models;
using CapitalApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CapitalApi.Tests
{
    public class TemplateTest
    {
        private readonly CosmosDbContext _dbContext;
        private readonly ITemplateRepository _templateRepository;

        private readonly IConfiguration _configuration;

        public TemplateTest()
        {
            _configuration = ConfigurationHelper.GetConfiguration();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<CosmosDbContext>((serviceProvider, options) =>
                {
                    var accountUri = _configuration["CosmosSettings:AccountUri"];
                    var accountKey = _configuration["CosmosSettings:AccountKey"];
                    var databaseName = _configuration["CosmosSettings:DatabaseName"];

                    if (accountUri == null || accountKey == null || databaseName == null)
                    {
                        throw new InvalidOperationException("One or more CosmosDB configuration values are missing.");
                    }

                    options.UseCosmos(accountUri, accountKey, databaseName);
                })
                .AddSingleton<IConfiguration>(_configuration) // Register IConfiguration as a singleton
                .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<CosmosDbContext>();
            _templateRepository = new TemplateRepository(_dbContext);
        }



        public static class ConfigurationHelper
        {
            public static IConfiguration GetConfiguration()
            {
                return new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
            }
        }



        [Fact]
        public async Task GetByIdAsync_TemplateExists_ReturnsTemplate()
        {
            // Arrange
            var templateId = Guid.NewGuid();
            var expectedTemplate = new Template { Id = templateId };
            _dbContext.Templates.Add(expectedTemplate);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _templateRepository.GetByIdAsync(templateId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(templateId, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_TemplateDoesNotExist_ReturnsNull()
        {
            // Arrange
            var templateId = Guid.NewGuid();

            // Act
            var result = await _templateRepository.GetByIdAsync(templateId);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public async Task AddAsync_AddsNewTemplate()
        {
            // Arrange
            var newTemplate = new Template { Id = Guid.NewGuid() };

            // Act
            await _templateRepository.AddAsync(newTemplate);

            // Assert
            var addedTemplate = await _dbContext.Templates.FindAsync(newTemplate.Id);
            Assert.NotNull(addedTemplate);
            Assert.Equal(newTemplate.Id, addedTemplate.Id);
        }


        [Fact]
        public async Task UpdateAsync_TemplateDoesNotExist_ThrowsException()
        {
            // Arrange
            var templateId = Guid.NewGuid();
            var updatedTemplate = new Template
            {
                Id = templateId,
                /* Set other properties as needed */
            };

            // Act and Assert
            var exception = await Assert.ThrowsAsync<DbUpdateException>(() => _templateRepository.UpdateAsync(updatedTemplate));
            Assert.Contains("An error occurred while saving", exception.Message);
        }

        [Fact]
        public async Task DeleteAsync_TemplateExists_DeletesTemplate()
        {
            // Arrange
            var templateId = Guid.NewGuid();
            var existingTemplate = new Template { Id = templateId, /* Set other properties */ };
            _dbContext.Templates.Add(existingTemplate);
            await _dbContext.SaveChangesAsync();

            // Act
            await _templateRepository.DeleteAsync(templateId);

            // Assert
            var deletedTemplate = await _dbContext.Templates.FindAsync(templateId);
            Assert.Null(deletedTemplate);
        }

        [Fact]
        public async Task DeleteAsync_TemplateDoesNotExist_ThrowsException()
        {
            // Arrange
            var templateId = Guid.NewGuid();

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _templateRepository.DeleteAsync(templateId));
        }


    }
}
