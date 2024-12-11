using Microsoft.EntityFrameworkCore;
using Serilog;
using Sodio_API_Task.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Logging
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().
    WriteTo.File("log/tasklogs.txt", rollingInterval:RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//using sql server instead of in memory
var connection = builder.Configuration.GetConnectionString("conn");
builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(connection);
});

//using in memory
//builder.Services.AddDbContext<TaskContext>(options =>
//    options.UseInMemoryDatabase("TaskDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
