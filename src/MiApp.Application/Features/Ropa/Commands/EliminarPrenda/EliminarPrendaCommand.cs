using MediatR;

namespace MiApp.Application.Features.Ropa.Commands.EliminarPrenda;

public class EliminarPrendaCommand : IRequest<EliminarPrendaResponse>
{
    public int Id { get; set; }
}
