using MediatR;

namespace MiApp.Application.Features.Ropa.Queries.ObtenerPrendaPorId;

public class ObtenerPrendaPorIdQuery : IRequest<ObtenerPrendaPorIdResponse>
{
    public int Id { get; set; }
}