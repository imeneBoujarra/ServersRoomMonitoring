using Microsoft.EntityFrameworkCore;
using Test.Data;
using System.Drawing;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddDbContext<AppDb_Context>(options =>
{
    options.UseSqlServer("Server=DESKTOP-S5AOCMI;Database=Leoni3;Trusted_Connection=True;TrustServerCertificate=True;");
});*/

builder.Services.AddDbContext<AppDb_Context>(options =>
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("myCon"));
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Add authorization services
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Include this if your requests involve credentials
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// Add authentication middleware if needed

app.UseAuthorization();

app.MapControllers();

app.Run();
