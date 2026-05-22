using MediatR;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Entities;
using MiApp.Domain.Exceptions;

namespace MiApp.Application.Features.Ropa.Commands.EliminarPrenda;

public class EliminarPrendaCommandHandler : IRequestHandler<EliminarPrendaCommand, EliminarPrendaResponse>
{
    private readonly IApplicationDbContext _context;

    public EliminarPrendaCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EliminarPrendaResponse> Handle(EliminarPrendaCommand request, CancellationToken cancellationToken)
    {
        var prenda = await _context.Prendas.FindAsync(new object[] { request.Id }, cancellationToken);

        if (prenda is null)
            throw new NotFoundException(nameof(Prenda), request.Id);

        _context.Prendas.Remove(prenda);
        await _context.SaveChangesAsync(cancellationToken);

        return new EliminarPrendaResponse
        {
            Exito = true,
            Mensaje = $"Prenda con ID {request.Id} eliminada correctamente."
        };
    }
}
