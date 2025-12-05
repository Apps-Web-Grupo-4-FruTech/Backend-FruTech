using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FruTech.Backend.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using FruTech.Backend.API.Shared.Domain.Settings;
using FruTech.Backend.API.Shared.Domain.Services;
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
using FruTech.Backend.API.Shared.Infrastructure.Services;
using Cortex.Mediator;

var builder = WebApplication.CreateBuilder(args);


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

// Configure TokenSettings
var tokenSettings = new TokenSettings();
builder.Configuration.GetSection("TokenSettings").Bind(tokenSettings);
builder.Services.AddSingleton(tokenSettings);

// Add JWT Authentication
var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = tokenSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = tokenSettings.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Register TokenService
builder.Services.AddScoped<ITokenService, FruTech.Backend.API.Shared.Infrastructure.Services.TokenService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IProgressHistoryRepository, ProgressHistoryRepository>();
builder.Services.AddScoped<ICropFieldRepository, CropFieldRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ICommunityRecommendationRepository, CommunityRecommendationRepository>();


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


builder.Services.AddHttpClient<IGeoLocationService, GeoLocationService>();


builder.Services.AddScoped<IMediator, Mediator>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
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

    // Agregar definición de seguridad JWT
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Ingresa tu token JWT en el formato: Bearer {token}"
    });

    // Aplicar el esquema de seguridad globalmente
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FruTech API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseCors(FrontendCorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
