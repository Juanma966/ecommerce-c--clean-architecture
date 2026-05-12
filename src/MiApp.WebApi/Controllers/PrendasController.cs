using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Ropa.Commands.CrearPrenda;
using MiApp.Application.Features.Ropa.Queries.ObtenerPrendaPorId;
using MiApp.Application.Features.Ropa.Queries.ObtenerTodasLasPrendas;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrendasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrendasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todas las prendas del catálogo.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<ObtenerTodasLasPrendasResponse>>> ObtenerTodas()
    {
        var resultado = await _mediator.Send(new ObtenerTodasLasPrendasQuery());
        return Ok(resultado);
    }

    /// <summary>
    /// Obtiene una prenda por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerPrendaPorIdResponse>> ObtenerPorId(int id)
    {
        try
        {
            var resultado = await _mediator.Send(new ObtenerPrendaPorIdQuery { Id = id });
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Crea una nueva prenda en el catálogo.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CrearPrendaResponse>> Crear([FromBody] CrearPrendaCommand command)
    {
        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.PrendaId }, resultado);
    }
}
