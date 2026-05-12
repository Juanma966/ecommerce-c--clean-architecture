namespace MiApp.Application.Features.Usuarios.Queries.ObtenerUsuarioPorId;

public class ObtenerUsuarioPorIdResponse
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public bool Activo { get; set; }
}