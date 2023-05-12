using Core;
using Core.Entities;
using Core.Interfaces;
using FantasticProps.Adapters;
using FantasticProps.Dtos;
using FantasticProps.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FantasticProps.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>>, ProductListAdapter>();
            services.AddScoped<IAdapter<Product, ProductToDto>, ProductToDtoAdapter>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddRouting(options => options.LowercaseUrls = true);


            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            });

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
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

            return services;
        }
    }
}
