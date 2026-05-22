using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Domain.Exceptions;

namespace MiApp.Application.Features.Ropa.Commands.ActualizarPrenda;

public class ActualizarPrendaCommandHandler : IRequestHandler<ActualizarPrendaCommand, ActualizarPrendaResponse>
{
    private readonly IApplicationDbContext _context;

    public ActualizarPrendaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ActualizarPrendaResponse> Handle(ActualizarPrendaCommand request, CancellationToken cancellationToken)
    {
        var prenda = await _context.Prendas.FindAsync(new object[] { request.Id }, cancellationToken);

        if (prenda is null)
            throw new NotFoundException(nameof(Prenda), request.Id);

        prenda.Nombre = request.Nombre;
        prenda.Descripcion = request.Descripcion;
        prenda.Talle = request.Talle;
        prenda.Color = request.Color;
        prenda.CategoryId = request.CategoryId;
        prenda.Precio = request.Precio;
        prenda.Stock = request.Stock;

        await _context.SaveChangesAsync(cancellationToken);

        return new ActualizarPrendaResponse
        {
            Exito = true,
            Mensaje = $"Prenda '{request.Nombre}' actualizada correctamente.",
            PrendaId = prenda.Id
        };
    }
}
