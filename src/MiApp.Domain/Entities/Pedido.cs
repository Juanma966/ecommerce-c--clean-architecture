namespace MiApp.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Pendiente";

    public Usuario Usuario { get; set; } = null!;
    public ICollection<PedidoDetalle> Detalles { get; set; } = new List<PedidoDetalle>();
}
