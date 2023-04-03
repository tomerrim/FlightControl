using FinalProject.Client.Hubs;
using FinalProject.DataAccess;
using FinalProject.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Dal>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<Dal>();
builder.Services.AddScoped<IRepository,Repository>();
builder.Services.AddScoped<FlightsHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           .SetIsOriginAllowed(origin => true)
           .AllowCredentials());

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.MapHub<FlightsHub>("/flightsHub");

app.Run();