using MediatR;

namespace MiApp.Application.Features.Pedidos.Queries.ObtenerPedidoPorId;

public class ObtenerPedidoPorIdQuery : IRequest<ObtenerPedidoPorIdResponse>
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
}
