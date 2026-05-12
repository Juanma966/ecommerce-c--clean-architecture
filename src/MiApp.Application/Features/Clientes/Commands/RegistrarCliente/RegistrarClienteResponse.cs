namespace MiApp.Application.Features.Clientes.Commands.RegistrarCliente;

public class RegistrarClienteResponse
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public int ClienteId { get; set; }
}