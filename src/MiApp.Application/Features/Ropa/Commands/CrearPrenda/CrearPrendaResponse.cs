namespace MiApp.Application.Features.Ropa.Commands.CrearPrenda;

public class CrearPrendaResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int PrendaId { get; set; } // Devuelve el ID generado
}