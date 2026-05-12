using MediatR;

namespace MiApp.Application.Features.Clientes.Commands.RegistrarCliente;

public class RegistrarClienteCommand : IRequest<RegistrarClienteResponse>
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}