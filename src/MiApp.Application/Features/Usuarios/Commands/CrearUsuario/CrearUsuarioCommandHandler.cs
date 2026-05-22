using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;

namespace MiApp.Application.Features.Usuarios.Commands.CrearUsuario;

public class CrearUsuarioCommandHandler : IRequestHandler<CrearUsuarioCommand, CrearUsuarioResponse>
{
    private readonly IApplicationDbContext _context;

    public CrearUsuarioCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CrearUsuarioResponse> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = new Usuario
        {
            NombreUsuario = request.NombreUsuario,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Rol = request.Rol,
            Activo = true
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        return new CrearUsuarioResponse
        {
            Exito = true,
            Mensaje = $"Usuario '{request.NombreUsuario}' creado correctamente con el rol de {request.Rol}.",
            UsuarioId = usuario.Id
        };
    }
}