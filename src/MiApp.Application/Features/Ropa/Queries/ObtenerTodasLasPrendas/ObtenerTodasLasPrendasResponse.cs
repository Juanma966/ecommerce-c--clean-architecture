namespace MiApp.Application.Features.Ropa.Queries.ObtenerTodasLasPrendas;

public class ObtenerTodasLasPrendasResponse
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int StockDisponible { get; set; }
}