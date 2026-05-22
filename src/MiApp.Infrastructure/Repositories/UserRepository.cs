using Microsoft.EntityFrameworkCore;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Infrastructure.Persistence.Contexts;

namespace MiApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
        => _context = context;

    public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
}
