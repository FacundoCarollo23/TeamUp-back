using Microsoft.EntityFrameworkCore;
using TeamUp.BLL.Service;
using TeamUp.BLL.sinNombre;
using TeamUp.DAL;
using TeamUp.DAL.Interfaces;
using TeamUp.DAL.Repository;
using TeamUp.Utility;
using TeamUp.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InyectarDependencias(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("TeamUpPolitica", app =>
    {
        app.WithOrigins("http://localhost:4200/")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("TeamUpPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
