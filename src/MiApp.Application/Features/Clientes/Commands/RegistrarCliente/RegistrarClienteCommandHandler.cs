using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;

namespace MiApp.Application.Features.Clientes.Commands.RegistrarCliente;

public class RegistrarClienteCommandHandler : IRequestHandler<RegistrarClienteCommand, RegistrarClienteResponse>
{
    private readonly IApplicationDbContext _context;

    public RegistrarClienteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RegistrarClienteResponse> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            Email = request.Email,
            Telefono = request.Telefono,
            Activo = true
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync(cancellationToken);

        return new RegistrarClienteResponse
        {
            Exito = true,
            Mensaje = $"Cliente {request.Nombre} {request.Apellido} registrado correctamente.",
            ClienteId = cliente.Id
        };
    }
}