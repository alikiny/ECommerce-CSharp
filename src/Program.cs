global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;

global using Backend.src.Services.ProductService;
global using Backend.src.Services.BaseService;
global using Backend.src.Services.UserService;
global using Backend.src.Services.OrderService;
global using Backend.src.Services.OrderItemService;
global using Backend.src.Services.CategoryService;
global using Backend.src.Dto;
global using Backend.src.Helpers;
global using Backend.src.Models;
global using Backend.src.Data;
global using Backend.src.Repository.BaseRepository;
global using Backend.src.Repository.ProductRepository;
global using Backend.src.Repository.UserRepository;
global using Backend.src.Repository.CategoryRepository;
global using Backend.src.Repository.OrderRepository;
global using Backend.src.Repository.ReviewRepository;
global using Backend.src.Repository.OrderItemRepository;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Backend.src.Services.AuthService;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Backend.src.Services.ReviewService;
using Backend.src.Milddlewares;
using Backend.src.Authorization;
using Microsoft.AspNetCore.Authorization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseKestrel(options =>
        {
            options.ListenLocalhost(5000);
            // Set HTTPS port
            options.ListenLocalhost(
                5001,
                listenOptions =>
                {
                    listenOptions.UseHttps();
                }
            );
        });
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    Description =
                        """Standard authorization using Bearer scheme, Example: "bearer {token}" """,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                }
            );
            /*             c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme{
                                    Reference = new OpenApiReference{
                                        Id = "Bearer", //The name of the previously defined security scheme.
                                        Type = ReferenceType.SecurityScheme
                                    }
                                },new List<string>()
                            }
                        }); */
            c.OperationFilter<SecurityRequirementsOperationFilter>(); //does not work on "Bearer" authorization scheme
        });

        // Add service scope
        builder.Services
            .AddScoped<IProductService, ProductService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IOrderItemService, OrderItemService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IReviewRepository, ReviewRepository>()
            .AddScoped<IOrderRepository, OrderRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IOrderItemRepository, OrderItemRepository>();

        builder.Services.AddSingleton<IAuthorizationHandler, ProductDeleteRequirementHandler>();

        //builder.Services.AddTransient<LoggingMiddleware>();
        builder.Services.AddTransient<ErrorHandlerMiddleware>();

        // Add auto mapper
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        //Add DatabaseContext
        builder.Services.AddDbContext<DatabaseContext>();

        //http accessor
        builder.Services.AddHttpContextAccessor();

        // Add authentication service
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:Token").Value!
                        )
                    )
                };
            });

        // Add authorization service
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnlyPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("SellerOnlyPolicy", policy => policy.RequireRole("Seller"));
            options.AddPolicy(
                "ProductDeletePolicy",
                policy => policy.AddRequirements(new ProductDeleteRequirement())
            );
        });

        var app = builder.Build();

        app.UseHttpsRedirection();
        // Configure the HTTP request pipeline.S
        /*         if (app.Environment.IsDevelopment())
                { */
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API");
            c.RoutePrefix = string.Empty;
        });
        /*   } */

        // app.UseMiddleware<LoggingMiddleware>();

        app.UseErrorHandler();

        app.UseLogging();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
