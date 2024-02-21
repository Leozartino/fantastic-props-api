using Core;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using FantasticProps.Adapters;
using FantasticProps.Dtos;
using FantasticProps.Helpers;
using FantasticProps.Validators;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FantasticProps.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IConfigurationSection jwtSettings)
        {
            services.AddScoped<IJwtSettingsHelper, JwtSetttingsHelper>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IValidator<ProductListRequest>, ProduListValidator>();
            services.AddScoped<IAdapter<IEnumerable<Product>, IEnumerable<ProductToDto>>, ProductListAdapter>();
            services.AddScoped<IAdapter<Product, ProductToDto>, ProductToDtoAdapter>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRouting(options => options.LowercaseUrls = true);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                                        .Where(e => e.Value.Errors.Count > 0)
                                        .ToDictionary(e => e.Key, e => e.Value.Errors.Select(error => error.ErrorMessage).ToArray());

                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Title = "Your request is not consistent, please review the following fields below",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "See the errors field for details.",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    problemDetails.Extensions.Add("errors", errors);

                    return new BadRequestObjectResult(problemDetails);
                };
            });

            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            });

            // IdentityUser -> represents the logged user
            // IdentityRole -> user profile (adm, common user, etc)
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreContext>();

            services.Configure<JwtSettings>(jwtSettings);

            var jwtConfig = jwtSettings.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidIssuer = jwtConfig.Issuer
                };
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

            services.AddCors(opt =>
            {
                opt.AddPolicy("Development", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });

                opt.AddPolicy("Production", policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins(configuration["Cors:ClientUri"]);
                });
            });

            return services;
        }
    }
}
