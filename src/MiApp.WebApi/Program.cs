using MiApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Agregar controllers
builder.Services.AddControllers();

// Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();
