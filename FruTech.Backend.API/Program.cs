using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using FruTech.Backend.API.Shared.Domain.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Application.Internal.CommandServices;
using FruTech.Backend.API.CommunityRecommendation.Application.Internal.QueryServices;
using FruTech.Backend.API.CommunityRecommendation.Domain.Repositories;
using FruTech.Backend.API.CommunityRecommendation.Domain.Services;
using FruTech.Backend.API.CommunityRecommendation.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Cortex.Mediator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for external access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Add Cortex Mediator
builder.Services.AddScoped<IMediator, Mediator>();

// Dependency Injection Configuration
// Shared Infrastructure
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Community Recommendation Context
builder.Services.AddScoped<ICommunityRecommendationRepository, CommunityRecommendationRepository>();
builder.Services.AddScoped<ICommunityRecommendationCommandService, CommunityRecommendationCommandService>();
builder.Services.AddScoped<ICommunityRecommendationQueryService, CommunityRecommendationQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
