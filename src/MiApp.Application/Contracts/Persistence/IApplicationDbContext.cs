using Microsoft.EntityFrameworkCore;
using MiApp.Domain.Entities;

namespace MiApp.Application.Contracts.Persistence;

public interface IApplicationDbContext
{
    DbSet<Prenda> Prendas { get; set; }
    DbSet<Cliente> Clientes { get; set; }
    DbSet<Usuario> Usuarios { get; set; }
    DbSet<Pedido> Pedidos { get; set; }
    DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
