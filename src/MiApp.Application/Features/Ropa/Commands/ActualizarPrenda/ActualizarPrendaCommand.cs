using MediatR;

namespace MiApp.Application.Features.Ropa.Commands.ActualizarPrenda;

public class ActualizarPrendaCommand : IRequest<ActualizarPrendaResponse>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Talle { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
}
