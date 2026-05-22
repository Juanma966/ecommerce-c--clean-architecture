namespace MiApp.Domain.Entities;

public class PedidoDetalle
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int PrendaId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }

    public Pedido Pedido { get; set; } = null!;
    public Prenda Prenda { get; set; } = null!;
}
