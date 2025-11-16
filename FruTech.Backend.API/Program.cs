using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Application.Internal.CommandServices;
using FruTech.Backend.API.User.Application.Internal.QueryServices;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Domain.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Domain.Services;
using FruTech.Backend.API.CommunityRecommendation.Application.Internal.CommandServices;
using FruTech.Backend.API.CommunityRecommendation.Application.Internal.QueryServices;
using FruTech.Backend.API.Tasks.Application.Internal.CommandServices;
using FruTech.Backend.API.Tasks.Application.Internal.QueryServices;
using FruTech.Backend.API.Tasks.Domain.Repositories;
using FruTech.Backend.API.Tasks.Domain.Services;
using FruTech.Backend.API.Tasks.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Fields.Domain.Services;
using FruTech.Backend.API.Fields.Application.Internal.CommandServices;
using FruTech.Backend.API.Fields.Application.Internal.QueryServices;
using FruTech.Backend.API.CropFields.Domain.Services;
using FruTech.Backend.API.CropFields.Application.Internal.CommandServices;
using FruTech.Backend.API.CropFields.Application.Internal.QueryServices;
using Cortex.Mediator;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
const string FrontendCorsPolicy = "FrontendCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DbContext MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "server=localhost;user=root;password=admin;database=frutech_database",
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection") 
            ?? "server=localhost;user=root;password=admin;database=frutech_database"
        )
    )
);

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IProgressHistoryRepository, ProgressHistoryRepository>();
builder.Services.AddScoped<ICropFieldRepository, CropFieldRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommunityRecommendationRepository, CommunityRecommendationRepository>();

// Services (Command & Query)
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IFieldCommandService, FieldCommandService>();
builder.Services.AddScoped<IFieldQueryService, FieldQueryService>();
builder.Services.AddScoped<ICropFieldCommandService, CropFieldCommandService>();
builder.Services.AddScoped<ICropFieldQueryService, CropFieldQueryService>();
builder.Services.AddScoped<ITaskCommandService, TaskCommandService>();
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();
builder.Services.AddScoped<ICommunityRecommendationCommandService, CommunityRecommendationCommandService>();
builder.Services.AddScoped<ICommunityRecommendationQueryService, CommunityRecommendationQueryService>();

// Mediator
builder.Services.AddScoped<IMediator, Mediator>();

// Controllers / OpenAPI
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "FruTech API", Version = "v1" });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Cleanup migration and UpcomingTasks tables if they exist (once at startup)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS `__EFMigrationsHistory`;");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS `UpcomingTasks`;");
        dbContext.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS `upcoming_tasks`;");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error cleaning up tables: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruTech API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);
app.UseAuthorization();
app.MapControllers();

app.Run();
