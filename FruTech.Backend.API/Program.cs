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
{
    app.MapOpenApi();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruTech API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
