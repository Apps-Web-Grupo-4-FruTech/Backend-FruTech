using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Application.Internal.CommandServices;
using FruTech.Backend.API.User.Application.Internal.QueryServices;
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories;
using FruTech.Backend.API.UpcomingTasks.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Services;
using FruTech.Backend.API.UpcomingTasks.Application.Internal.CommandServices;
using FruTech.Backend.API.UpcomingTasks.Application.Internal.QueryServices;
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
using Microsoft.EntityFrameworkCore;
using Cortex.Mediator;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
const string FrontendCorsPolicy = "FrontendCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DbContext MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "server=localhost;user=root;password=admin;database=frutech_database"
    )
);

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUpcomingTaskRepository, UpcomingTaskRepository>();
builder.Services.AddScoped<ICropFieldRepository, CropFieldRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IProgressHistoryRepository, ProgressHistoryRepository>();
builder.Services.AddScoped<ICommunityRecommendationRepository, CommunityRecommendationRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Services
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUpcomingTaskCommandService, UpcomingTaskCommandService>();
builder.Services.AddScoped<IUpcomingTaskQueryService, UpcomingTaskQueryService>();
builder.Services.AddScoped<ICommunityRecommendationCommandService, CommunityRecommendationCommandService>();
builder.Services.AddScoped<ICommunityRecommendationQueryService, CommunityRecommendationQueryService>();
builder.Services.AddScoped<ITaskCommandService, TaskCommandService>();
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create database automatically
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear la base de datos: {ex.Message}");
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
