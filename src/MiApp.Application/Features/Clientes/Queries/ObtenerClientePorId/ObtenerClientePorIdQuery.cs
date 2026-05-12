using MediatR;

namespace MiApp.Application.Features.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdQuery : IRequest<ObtenerClientePorIdResponse>
{
    public int Id { get; set; }
}