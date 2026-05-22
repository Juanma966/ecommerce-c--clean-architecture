using MiApp.Domain.Entities;

namespace MiApp.Application.Contracts.Persistence;

public interface IUserRepository
{
    Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario);
}
