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
global using Backend.src.Dto;
global using Backend.src.Helpers;
global using Backend.src.Models;
global using Backend.src.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add service scope
        builder.Services
        .AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>))
        .AddScoped<IProductService, ProductService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IOrderService, OrderService>()
        .AddScoped<IOrderItemService, OrderItemService>();

        // Add auto mapper
        builder.Services.AddAutoMapper(typeof(Program).Assembly);

        //Add DatabaseContext
        builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.S
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API");
                c.RoutePrefix = "";
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}