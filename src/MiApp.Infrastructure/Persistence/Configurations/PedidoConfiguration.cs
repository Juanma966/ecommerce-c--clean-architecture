using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Fecha).IsRequired();
        builder.Property(p => p.Total).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Estado).IsRequired().HasMaxLength(50);

        builder.HasOne(p => p.Usuario)
               .WithMany()
               .HasForeignKey(p => p.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
