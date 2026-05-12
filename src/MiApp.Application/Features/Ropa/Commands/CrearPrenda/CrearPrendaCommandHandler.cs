using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;

namespace MiApp.Application.Features.Ropa.Commands.CrearPrenda;

public class CrearPrendaCommandHandler : IRequestHandler<CrearPrendaCommand, CrearPrendaResponse>
{
    private readonly IApplicationDbContext _context;

    public CrearPrendaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CrearPrendaResponse> Handle(CrearPrendaCommand request, CancellationToken cancellationToken)
    {
        var prenda = new Prenda
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Talle = request.Talle,
            Color = request.Color,
            CategoryId = request.CategoryId,
            Precio = request.Precio,
            Stock = request.StockInicial
        };

        _context.Prendas.Add(prenda);
        await _context.SaveChangesAsync(cancellationToken);

        return new CrearPrendaResponse
        {
            Exito = true,
            Mensaje = $"Se agregó correctamente la prenda: {request.Nombre} (Talle: {request.Talle})",
            PrendaId = prenda.Id
        };
    }
}