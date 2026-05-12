namespace MiApp.Application.Features.Clientes.Queries.ObtenerClientePorId;

public class ObtenerClientePorIdResponse
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public bool Activo { get; set; }
}