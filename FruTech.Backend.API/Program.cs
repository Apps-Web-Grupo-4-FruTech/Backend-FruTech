using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;
using FruTech.Backend.API.User.Domain.Repositories;
using FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.User.Domain.Services;
using FruTech.Backend.API.User.Application.Internal.CommandServices;
using FruTech.Backend.API.User.Application.Internal.QueryServices;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories;
using FruTech.Backend.API.UpcomingTasks.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.UpcomingTasks.Domain.Services;
using FruTech.Backend.API.UpcomingTasks.Application.Internal.CommandServices;
using FruTech.Backend.API.UpcomingTasks.Application.Internal.QueryServices;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CORS para el frontend
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
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "server=localhost;user=root;password=admin;database=frutech_database"));

// Unidad de trabajo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUpcomingTaskRepository, UpcomingTaskRepository>();
builder.Services.AddScoped<ICropFieldRepository, CropFieldRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IProgressHistoryRepository, ProgressHistoryRepository>();

// Services
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUpcomingTaskCommandService, UpcomingTaskCommandService>();
builder.Services.AddScoped<IUpcomingTaskQueryService, UpcomingTaskQueryService>();

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

// Crear la base de datos automáticamente
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
app.MapControllers();

app.Run();
