using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using CapitalApi.Cosmos;
using Microsoft.EntityFrameworkCore;
using CapitalApi.Repositories;
using AutoMapper;
using CapitalApi.Dto;
using Microsoft.AspNetCore.Http;
using CapitalApi.Models;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "Capital Placement Minimal API",
         Description = "Making the in house tools you love",
         Version = "v1" });
});

builder.Services.AddDbContext<CosmosDbContext>(opt =>
{
    var accountUri = builder.Configuration["CosmosSettings:AccountUri"];
    var accountKey = builder.Configuration["CosmosSettings:AccountKey"];
    var databaseName = builder.Configuration["CosmosSettings:DatabaseName"];

    if (accountUri != null && accountKey != null && databaseName != null)
    {
        opt.UseCosmos(accountUri, accountKey, databaseName);
    }
    else
    {
        // Handle the case where one of the required configuration values is null.
        // Log an error and throw an exception or take appropriate action.
        if (accountUri == null)
        {
            // Log an error for missing accountUri.
            Console.WriteLine("Error: CosmosSettings:AccountUri is missing.");
        }

        if (accountKey == null)
        {
            // Log an error for missing accountKey.
            Console.WriteLine("Error: CosmosSettings:AccountKey is missing.");
        }

        if (databaseName == null)
        {
            // Log an error for missing databaseName.
            Console.WriteLine("Error: CosmosSettings:DatabaseName is missing.");
        }

        // You can throw an exception or take other appropriate action here.
        // For example, throwing an exception:
        throw new InvalidOperationException("One or more required CosmosDB configuration values are missing.");
    }
});


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProgramRepository, ProgramRepository>();
builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();



var app = builder.Build();
    
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1");
});

app.UseHttpsRedirection();

// Automatically create db on POST
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CosmosDbContext>();
    dbContext.Database.EnsureCreated();
}


    
app.MapGet("api/programs", async (IProgramRepository repository, IMapper mapper) =>
{
    var programModels = await repository.GetAllAsync();
    return Results.Ok(mapper.Map<IEnumerable<ProgramDTO>>(programModels));
});

app.MapGet("api/programs/{id:guid}", async (IProgramRepository repository, IMapper mapper, Guid id) =>
{
    var command = await repository.GetByIdAsync(id);
    if (command != null)
    {
        return Results.Ok(mapper.Map<ProgramDTO>(command));
    }

    return Results.NotFound();
});

app.MapPost("api/programs", async (IProgramRepository repository, IMapper mapper, ProgramDTO programdto) =>
{
    var programModel = mapper.Map<ProgramModel>(programdto);

    await repository.AddAsync(programModel);

    var programReadDto = mapper.Map<ProgramDTO>(programModel);

    return Results.Created($"api/programs/{programReadDto.Id}", programReadDto);
});

app.MapPut("api/programs/{id:guid}",
    async (IProgramRepository repository, IMapper mapper, Guid id, ProgramDTO cmdUpdateDto) =>
    {
        var command = await repository.GetByIdAsync(id);
        if (command == null)
        {
            return Results.NotFound();
        }

        mapper.Map(cmdUpdateDto, command);

        await repository.UpdateAsync(command);

        return Results.Ok();
});

app.MapGet("api/templates", async (ITemplateRepository repository, IMapper mapper) =>
{
    var templates = await repository.GetAllAsync();
    return Results.Ok(templates);
});

app.MapGet("api/templates/{id:guid}", async (ITemplateRepository repository, IMapper mapper, Guid id) =>
{
    var template = await repository.GetByIdAsync(id);

    if (template != null)
    {
        return Results.Ok(template);
    }

    return Results.NotFound();
});

app.MapPut("api/templates/{id:guid}",
    async (ITemplateRepository repository, IMapper mapper, Guid id, TemplateDTO cmdUpdateDto) =>
    {
        var command = await repository.GetByIdAsync(id);
        if (command == null)
        {
            return Results.NotFound();
        }

        mapper.Map(cmdUpdateDto, command);

        await repository.UpdateAsync(command);

        return Results.Ok();
});

app.MapGet("api/workflows", async ([FromServices] IWorkflowRepository repository) =>
{
    var workflows = await repository.GetWorkflowsForStagesAsync();
    return Results.Ok(workflows);
}).Produces<List<WorkflowDTO>>();



app.MapPut("api/workflows/{id}/{newStage}", async ([FromServices]Guid id, WorkflowStage newStage, IWorkflowRepository repository) =>
    {
        var success = await repository.UpdateWorkflowStageAsync(id, newStage);
        if (success)
        {
            return Results.Ok("Workflow stage updated successfully.");
        }
        else
        {
            return Results.NotFound("Workflow not found.");
        }
    });
    
app.Run();