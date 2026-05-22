using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiApp.Application.Contracts.Persistence;
using MiApp.Application.Interfaces;
using MiApp.Infrastructure.Persistence.Contexts;
using MiApp.Infrastructure.Repositories;
using MiApp.Infrastructure.Services;

namespace MiApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Registrar el DbContext con SQLite
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Registrar la interfaz del DbContext
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        // Registrar repositorios
        services.AddScoped<IUserRepository, UserRepository>();

        // Registrar servicios
        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }
}
