using EmployeesManagement.API.Midlewares;
using EmployeesManagement.Application.Helpers;
using EmployeesManagement.Application.Validations;
using EmployeesManagement.Infrastructure;
using EmployeesManagement.Infrastructure.Data;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
//builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});


builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();

// Registering Infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddOpenApi();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employees Management API " + builder.Environment.EnvironmentName,
        Version = "V1",
        Description = "This API built using ASP.Net Core Web API framework (.Net 9) to manage Employees operations.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Test",
            Email = "Test",
            Url = new Uri("https://github.com/ZakariaBakhkhouch"),
        },
        License = new OpenApiLicense
        {
            Name = "Test",
            Url = new Uri("https://example.com/license"),
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    //// Add support for XML comments
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

// Register MediatR services
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); // Current project
    cfg.RegisterServicesFromAssembly(typeof(MediatREntryPoint).Assembly); // Application project assembly
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    
}

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.MapOpenApi();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeesManagement API V1");
    });

    // enable developer exception page
    app.UseDeveloperExceptionPage();

    // seed data
    DBInitializer.SeedDataAsync(app);
}

// exception handling
app.UseMiddleware<ExceptionsMiddleware>();
//app.UseMiddleware<OtherCustomMidlewares>();

// static files
app.UseStaticFiles();

app.UseRouting();

// CORS
app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
