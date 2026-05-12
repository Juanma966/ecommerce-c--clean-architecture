using MediatR;
using Microsoft.EntityFrameworkCore; // Necesario para .Include() y .FirstOrDefaultAsync()
using MiApp.Application.Contracts.Persistence;

namespace MiApp.Application.Features.Ropa.Queries.ObtenerPrendaPorId;

public class ObtenerPrendaPorIdQueryHandler : IRequestHandler<ObtenerPrendaPorIdQuery, ObtenerPrendaPorIdResponse>
{
    private readonly IApplicationDbContext _context;

    public ObtenerPrendaPorIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ObtenerPrendaPorIdResponse> Handle(ObtenerPrendaPorIdQuery request, CancellationToken cancellationToken)
    {
        // CAMBIO: Usamos FirstOrDefaultAsync con Include para traer los datos de la categoría
        var prenda = await _context.Prendas
            .Include(p => p.Category) 
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (prenda == null)
            throw new Exception($"No se encontró la prenda con ID {request.Id}.");

        return new ObtenerPrendaPorIdResponse
        {
            Id = prenda.Id,
            Nombre = prenda.Nombre,
            Descripcion = prenda.Descripcion,
            Talle = prenda.Talle,
            Color = prenda.Color,
            // CAMBIO: Accedemos al nombre a través de la propiedad de navegación
            Categoria = prenda.Category?.Name ?? "Sin Categoría", 
            Precio = prenda.Precio,
            StockDisponible = prenda.Stock
        };
    }
}