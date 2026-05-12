using MediatR;

namespace MiApp.Application.Features.Usuarios.Commands.CrearUsuario;

public class CrearUsuarioCommand : IRequest<CrearUsuarioResponse>
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // Acá viene la clave en crudo desde el frontend
    public string Rol { get; set; } = string.Empty;
}