using MediatR;

namespace MiApp.Application.Features.Usuarios.Queries.ObtenerUsuarioPorId;

public class ObtenerUsuarioPorIdQuery : IRequest<ObtenerUsuarioPorIdResponse>
{
    public int Id { get; set; }
}