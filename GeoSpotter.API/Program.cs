using GeoSpotter.API.Clients;
using GeoSpotter.API.Data;
using GeoSpotter.API.Persistence.Interfaces;
using GeoSpotter.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:GeoSpotterConnection"]));
builder.Services.AddScoped<ApplicationDbContextFactory>();

builder.Services.AddScoped<IFoursquareClient, FoursquareClient>();
builder.Services.AddScoped<IApiMessageRepository, ApiMessageRepository>();
builder.Services.AddScoped<IFavouriteLocationRepository, FavouriteLocationRepository>();

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
