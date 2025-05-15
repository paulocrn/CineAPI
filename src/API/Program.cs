using API.Extensions;
using Application.Exceptions;
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CineDbContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionPrincipal"),
        opcionesSql => opcionesSql.MigrationsAssembly(typeof(CineDbContext).Assembly.FullName));
    
    if (builder.Environment.IsDevelopment())
    {
        opciones.EnableSensitiveDataLogging();
        opciones.EnableDetailedErrors();
    }
});

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(e => e.Value!.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return new BadRequestObjectResult(new ErrorResponse(
                "Error de validación", 
                StatusCodes.Status400BadRequest, 
                "Uno o más errores de validación ocurrieron")
            {
                Errores = errors
            });
        };
    });



builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CineAPI", Version = "v1" });
});

//builder.Services.AddOpenApi();


builder.Services.AddScoped<IRepositorioAsiento, RepositorioAsiento>();
builder.Services.AddScoped<IRepositorioCartelera, RepositorioCartelera>();
builder.Services.AddScoped<IRepositorioCliente, RepositorioCliente>();
builder.Services.AddScoped<IRepositorioPelicula, RepositorioPelicula>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<IRepositorioSala, RepositorioSala>();


builder.Services.AddScoped<IServicioCartelera, ServicioCartelera>();
builder.Services.AddScoped<IServicioAsiento, ServicioAsiento>();
builder.Services.AddScoped<IServicioSala, ServicioSala>();
builder.Services.AddScoped<IServicioReserva, ServicioReserva>();
builder.Services.AddScoped<IServicioPelicula, ServicioPelicula>();
builder.Services.AddScoped<IServicioCliente, ServicioCliente>();

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();

builder.Services.AddScoped<Application.Exceptions.IExceptionHandler, Application.Exceptions.GlobalExceptionHandler>();


// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(PerfilAutoMapper));



var app = builder.Build();

// Usa la política CORS
app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<CineDbContext>();

        context.Database.EnsureDeleted(); // Elimina completamente la BD
        context.Database.EnsureCreated(); // Crea esquema desde cero

        await DatabaseSeeder.SeedAsync(services);
    }
}


app.UseHttpsRedirection();

app.UseRouting();

app.Use(async (context, next) =>
{
    var exceptionHandler = context.RequestServices.GetRequiredService<Application.Exceptions.IExceptionHandler>();

    try
    {
        await next();
    }
    catch (Exception ex)
    {
        await exceptionHandler.TryHandleAsync(context, ex, context.RequestAborted);
    }
});

app.MapControllers();

app.Run();

