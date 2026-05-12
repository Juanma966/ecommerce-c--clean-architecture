using MediatR;
using MiApp.Application.Contracts.Persistence;

namespace MiApp.Application.Features.Usuarios.Queries.ObtenerUsuarioPorId;

public class ObtenerUsuarioPorIdQueryHandler : IRequestHandler<ObtenerUsuarioPorIdQuery, ObtenerUsuarioPorIdResponse>
{
    private readonly IApplicationDbContext _context;

    public ObtenerUsuarioPorIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObtenerUsuarioPorIdResponse> Handle(ObtenerUsuarioPorIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FindAsync(new object[] { request.Id }, cancellationToken);

        if (usuario == null)
            throw new Exception($"No se encontró el usuario con ID {request.Id}.");

        return new ObtenerUsuarioPorIdResponse
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Activo = usuario.Activo
        };
    }
}