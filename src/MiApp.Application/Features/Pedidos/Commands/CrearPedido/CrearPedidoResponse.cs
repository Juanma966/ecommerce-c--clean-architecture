namespace MiApp.Application.Features.Pedidos.Commands.CrearPedido;

public class CrearPedidoResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int PedidoId { get; set; }
    public decimal Total { get; set; }
}
