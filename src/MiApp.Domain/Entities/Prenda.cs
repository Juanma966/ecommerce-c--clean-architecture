namespace MiApp.Domain.Entities;

public class Prenda
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Talle { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    // --- CAMBIO AQUÍ: Relación con Category ---
    
    // Usamos Guid porque en CategoryConfiguration definimos el ID como Guid
    public Guid CategoryId { get; set; } 
    
    // Propiedad de navegación
    public Category Category { get; set; } = null!;

    // --- Fin del cambio ---

    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public void DescontarStock(int cantidad) 
    { 
        if (cantidad <= Stock) Stock -= cantidad; 
    }
}