using AluraMovieApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString  = builder.Configuration.GetConnectionString("MovieConnection");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {c.SwaggerDoc("v1", new OpenApiInfo {Title = "MovieAPI", Version = "V1"});
                                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                                    c.IncludeXmlComments(xmlPath);});

                
builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });                                    

builder.Services.AddDbContext<MovieContext>(options => options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();