using MediatR;
using Microsoft.EntityFrameworkCore;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Domain.Exceptions;

namespace MiApp.Application.Features.Pedidos.Queries.ObtenerPedidoPorId;

public class ObtenerPedidoPorIdQueryHandler : IRequestHandler<ObtenerPedidoPorIdQuery, ObtenerPedidoPorIdResponse>
{
    private readonly IApplicationDbContext _context;

    public ObtenerPedidoPorIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObtenerPedidoPorIdResponse> Handle(ObtenerPedidoPorIdQuery request, CancellationToken cancellationToken)
    {
        var pedido = await _context.Pedidos
            .Include(p => p.Detalles)
                .ThenInclude(d => d.Prenda)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (pedido is null)
            throw new NotFoundException(nameof(Pedido), request.Id);

        if (pedido.UsuarioId != request.UsuarioId)
            throw new DomainRuleException("No tienes permisos para ver este pedido.");

        return new ObtenerPedidoPorIdResponse
        {
            Id = pedido.Id,
            Fecha = pedido.Fecha,
            Estado = pedido.Estado,
            Total = pedido.Total,
            Items = pedido.Detalles.Select(d => new PedidoItemDto
            {
                PrendaId = d.PrendaId,
                NombrePrenda = d.Prenda.Nombre,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.PrecioUnitario * d.Cantidad
            }).ToList()
        };
    }
}
