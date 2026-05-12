namespace MiApp.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Una categoría tiene muchas prendas
    public ICollection<Prenda> Prendas { get; set; } = new List<Prenda>();
}