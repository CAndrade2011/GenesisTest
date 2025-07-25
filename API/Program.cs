using Application.Services.CDB;
using Domain.Services;
using Domain.Services.Calculadoras;
using API.SwaggerExamples;
using API.Extensions;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS para permitir qualquer origem
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy  =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICdbAppService, CdbAppService>();
builder.Services.AddScoped<ICdbCalculadora, CdbCalculadora>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ExampleFilters();
    
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "API-Key",
        Description = "API Key para autenticação. Use: Genesis-API-Key-2025",
        Scheme = "ApiKeyScheme"
    });

    var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    };

    var requirement = new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };

    c.AddSecurityRequirement(requirement);
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Genesis CDB Calculadora API",
        Version = "v1",
        Description = "API para cálculo de rendimento de CDB",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Genesis Team",
            Email = "carlos.andrade@genesis.com"
        }
    });
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CdbCommandExample>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Adicionar o middleware de CORS antes de Authorization e MapControllers
app.UseCors(MyAllowSpecificOrigins);

app.UseApiKeyValidation();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
