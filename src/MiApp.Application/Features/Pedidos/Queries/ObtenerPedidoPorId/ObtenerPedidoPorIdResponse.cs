namespace MiApp.Application.Features.Pedidos.Queries.ObtenerPedidoPorId;

public class ObtenerPedidoPorIdResponse
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public List<PedidoItemDto> Items { get; set; } = new();
}

public class PedidoItemDto
{
    public int PrendaId { get; set; }
    public string NombrePrenda { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
