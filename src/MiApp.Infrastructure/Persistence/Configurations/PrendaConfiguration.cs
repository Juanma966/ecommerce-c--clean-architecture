using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiApp.Domain.Entities;

namespace MiApp.Infrastructure.Persistence.Configurations;

public class PrendaConfiguration : IEntityTypeConfiguration<Prenda>
{
    public void Configure(EntityTypeBuilder<Prenda> builder)
    {
        // 1. Nombre de la tabla
        builder.ToTable("Prendas");

        // 2. Clave primaria
        builder.HasKey(p => p.Id);

        // 3. Propiedades y validaciones
        builder.Property(p => p.Nombre)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(p => p.Descripcion)
               .HasMaxLength(500); // Opcional: limitar el largo de la descripción

        builder.Property(p => p.Precio)
               .HasColumnType("decimal(18,2)");

        // 4. Configuración de la Relación (1 Categoría -> Muchas Prendas)
        builder.HasOne(p => p.Category)      // Una prenda tiene una categoría
               .WithMany(c => c.Prendas)    // Una categoría tiene muchas prendas (Products)
               .HasForeignKey(p => p.CategoryId) // La clave es CategoryId
               .OnDelete(DeleteBehavior.Restrict); // No permite borrar categorías si tienen prendas
    }
}