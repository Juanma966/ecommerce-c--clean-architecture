namespace MiApp.Application.Features.Ropa.Commands.ActualizarPrenda;

public class ActualizarPrendaResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int PrendaId { get; set; }
}
