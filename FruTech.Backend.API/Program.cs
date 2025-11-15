using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration; // agregado
using Microsoft.EntityFrameworkCore; // agregado
using FruTech.Backend.API.User.Domain.Repositories; // agregado
using FruTech.Backend.API.User.Infrastructure.Persistence.EFC.Repositories; // agregado
using FruTech.Backend.API.User.Domain.Services; // agregado
using FruTech.Backend.API.User.Application.Internal.CommandServices; // agregado
using FruTech.Backend.API.User.Application.Internal.QueryServices; // agregado
using FruTech.Backend.API.Shared.Domain.Repositories; // agregado
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories; // agregado
using FruTech.Backend.API.UpcomingTasks.Domain.Repositories; // nuevo using
using FruTech.Backend.API.UpcomingTasks.Infrastructure.Persistence.EFC.Repositories; // nuevo using
using FruTech.Backend.API.UpcomingTasks.Domain.Services; // nuevo using
using FruTech.Backend.API.UpcomingTasks.Application.Internal.CommandServices; // nuevo using
using FruTech.Backend.API.UpcomingTasks.Application.Internal.QueryServices; // nuevo using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext registration (MySQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL("database=frutech_database"));

// Repositories & UnitOfWork
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUpcomingTaskRepository, UpcomingTaskRepository>(); // nuevo
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IUpcomingTaskCommandService, UpcomingTaskCommandService>(); // nuevo
builder.Services.AddScoped<IUpcomingTaskQueryService, UpcomingTaskQueryService>(); // nuevo

var app = builder.Build();

if (app.Environment.IsDevelopment())
using WebApplication = Microsoft.AspNetCore.Builder.WebApplication;
using FruTech.Backend.API.CropFields.Domain.Model.Repositories;
using FruTech.Backend.API.CropFields.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Fields.Domain.Model.Repositories;
using FruTech.Backend.API.Fields.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<ICropFieldRepository, CropFieldRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IProgressHistoryRepository, ProgressHistoryRepository>();

// Controllers / OpenAPI (Swagger)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        // Permitir múltiples formatos de fecha sin conversión estricta
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crear la base de datos y tablas automáticamente si no existen
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("No se crea las tablas y bases de datos");
    }
}

<<<<<<< HEAD
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruTech API V1");
    c.RoutePrefix = "swagger";
=======
// Activar Swagger UI en todos los entornos para facilitar pruebas
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruTech API V1");
    c.RoutePrefix = "swagger"; // accesible en /swagger
>>>>>>> fields
});

app.UseHttpsRedirection();

<<<<<<< HEAD
=======
// Habilitar CORS
app.UseCors(FrontendCorsPolicy);

// Mapear controllers
>>>>>>> fields
app.MapControllers();

app.Run();
