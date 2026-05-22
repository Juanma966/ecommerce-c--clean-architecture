using MediatR;
using Microsoft.EntityFrameworkCore;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Domain.Exceptions;

namespace MiApp.Application.Features.Pedidos.Commands.CrearPedido;

public class CrearPedidoCommandHandler : IRequestHandler<CrearPedidoCommand, CrearPedidoResponse>
{
    private readonly IApplicationDbContext _context;

    public CrearPedidoCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CrearPedidoResponse> Handle(CrearPedidoCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FindAsync(new object[] { request.UsuarioId }, cancellationToken);
        if (usuario is null)
            throw new NotFoundException(nameof(Usuario), request.UsuarioId);

        var prendasIds = request.Items.Select(i => i.PrendaId).ToList();
        var prendas = await _context.Prendas
            .Where(p => prendasIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var pedido = new Pedido
        {
            UsuarioId = request.UsuarioId,
            Fecha = DateTime.UtcNow,
            Estado = "Pendiente"
        };

        decimal total = 0;

        foreach (var item in request.Items)
        {
            var prenda = prendas.FirstOrDefault(p => p.Id == item.PrendaId)
                ?? throw new NotFoundException(nameof(Prenda), item.PrendaId);

            if (prenda.Stock < item.Cantidad)
                throw new InsufficientStockException(item.Cantidad, prenda.Stock);

            prenda.DescontarStock(item.Cantidad);

            pedido.Detalles.Add(new PedidoDetalle
            {
                PrendaId = item.PrendaId,
                Cantidad = item.Cantidad,
                PrecioUnitario = prenda.Precio
            });

            total += prenda.Precio * item.Cantidad;
        }

        pedido.Total = total;

        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync(cancellationToken);

        return new CrearPedidoResponse
        {
            Exito = true,
            Mensaje = $"Pedido #{pedido.Id} creado correctamente.",
            PedidoId = pedido.Id,
            Total = pedido.Total
        };
    }
}
