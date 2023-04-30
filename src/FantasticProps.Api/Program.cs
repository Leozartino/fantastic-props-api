using System;
using System.Collections.Generic;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(options =>
  {
    options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
  });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
  options.SwaggerDoc("v1", new OpenApiInfo
  {
    Version = "v1",
    Title = "FantasticProp API",
    Description = "An ASP.NET Core Web API for manage an e-commerce website",
    Contact = new OpenApiContact
    {
      Name = "Leonardo Oliveira",
      Url = new Uri(uriString: configuration["SwaggerUris:ContactUri"])
    },
    License = new OpenApiLicense
    {
      Name = "MIT License",
      Url = new Uri(uriString: configuration["SwaggerUris:LicenseUri"])
    }
  });

  options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
    {
        new OpenApiSecurityScheme
        {
            Name = "Bearer",
            In = ParameterLocation.Header,
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
            }
        },
        new List<string>()
    }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger(options =>
  {
    options.SerializeAsV2 = true;
  });

  app.UseSwaggerUI(options =>
  {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
  });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<StoreContext>();
  var loggerFactory = services.GetRequiredService<ILoggerFactory>();
  try
  {
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
  }
  catch (Exception exception)
  {
    var logger = loggerFactory.CreateLogger<Program>();
    logger.LogError(exception, "An error occured during migration");
  }
}

app.Run();
