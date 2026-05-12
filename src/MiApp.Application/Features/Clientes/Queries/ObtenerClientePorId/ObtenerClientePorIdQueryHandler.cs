using MediatR;
using MiApp.Application.Contracts.Persistence;

namespace MiApp.Application.Features.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdQueryHandler : IRequestHandler<ObtenerClientePorIdQuery, ObtenerClientePorIdResponse>
{
    private readonly IApplicationDbContext _context;

    public ObtenerClientePorIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObtenerClientePorIdResponse> Handle(ObtenerClientePorIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _context.Clientes.FindAsync(new object[] { request.Id }, cancellationToken);

        if (cliente == null)
            throw new Exception($"No se encontró el cliente con ID {request.Id}.");

        return new ObtenerClientePorIdResponse
        {
            Id = cliente.Id,
            NombreCompleto = $"{cliente.Nombre} {cliente.Apellido}",
            Email = cliente.Email,
            Telefono = cliente.Telefono,
            Activo = cliente.Activo
        };
    }
}