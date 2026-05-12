namespace MiApp.Application.Features.Usuarios.Commands.CrearUsuario;

public class CrearUsuarioResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
}