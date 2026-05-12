using MediatR;

namespace MiApp.Application.Features.Ropa.Commands.CrearPrenda;

public class CrearPrendaCommand : IRequest<CrearPrendaResponse>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    
    public string Talle { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

    // CAMBIO AQUÍ: Ahora recibimos el ID único de la categoría, no el nombre
    public Guid CategoryId { get; set; } 
    
    public decimal Precio { get; set; }
    public int StockInicial { get; set; }
}