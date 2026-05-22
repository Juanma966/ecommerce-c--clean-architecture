using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence.Configurations;

public class PedidoDetalleConfiguration : IEntityTypeConfiguration<PedidoDetalle>
{
    public void Configure(EntityTypeBuilder<PedidoDetalle> builder)
    {
        builder.ToTable("PedidoDetalles");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Cantidad).IsRequired();
        builder.Property(d => d.PrecioUnitario).HasColumnType("decimal(18,2)");

        builder.HasOne(d => d.Pedido)
               .WithMany(p => p.Detalles)
               .HasForeignKey(d => d.PedidoId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.Prenda)
               .WithMany()
               .HasForeignKey(d => d.PrendaId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
