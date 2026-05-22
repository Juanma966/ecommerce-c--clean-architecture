using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiApp.Infrastructure;
using MiApp.Infrastructure.Middleware;
using MiApp.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Agregar controllers
builder.Services.AddControllers();

// Agregar GlobalExceptionHandler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = config["Jwt:Issuer"],
            ValidateAudience         = true,
            ValidAudience            = config["Jwt:Audience"],
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Registrar MediatR (escanea el assembly de Application para encontrar los handlers)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(MiApp.Application.Contracts.Persistence.IApplicationDbContext).Assembly));

// Registrar Infrastructure (DbContext, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// Configurar CORS para permitir conexión desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Crear la base de datos automáticamente al iniciar (solo para desarrollo)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MiApp.Infrastructure.Persistence.Contexts.ApplicationDbContext>();
    db.Database.EnsureCreated();

    // Seed: usuarios de prueba
    if (!db.Usuarios.Any(u => u.NombreUsuario == "test"))
    {
        db.Usuarios.Add(new Usuario
        {
            NombreUsuario = "test",
            Email         = "test@test.com",
            PasswordHash  = BCrypt.Net.BCrypt.HashPassword("test1"),
            Rol           = "Admin",
            Activo        = true
        });
    }

    if (!db.Usuarios.Any(u => u.NombreUsuario == "usuario"))
    {
        db.Usuarios.Add(new Usuario
        {
            NombreUsuario = "usuario",
            Email         = "usuario@test.com",
            PasswordHash  = BCrypt.Net.BCrypt.HashPassword("usuario1"),
            Rol           = "User",
            Activo        = true
        });
    }

    db.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
