using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Ropa.Commands.ActualizarPrenda;
using MiApp.Application.Features.Ropa.Commands.CrearPrenda;
using MiApp.Application.Features.Ropa.Commands.EliminarPrenda;
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

    [HttpGet]
    public async Task<ActionResult<List<ObtenerTodasLasPrendasResponse>>> ObtenerTodas()
    {
        var resultado = await _mediator.Send(new ObtenerTodasLasPrendasQuery());
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerPrendaPorIdResponse>> ObtenerPorId(int id)
    {
        var resultado = await _mediator.Send(new ObtenerPrendaPorIdQuery { Id = id });
        return Ok(resultado);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CrearPrendaResponse>> Crear([FromBody] CrearPrendaCommand command)
    {
        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.PrendaId }, resultado);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ActualizarPrendaResponse>> Actualizar(int id, [FromBody] ActualizarPrendaCommand command)
    {
        command.Id = id;
        var resultado = await _mediator.Send(command);
        return Ok(resultado);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<EliminarPrendaResponse>> Eliminar(int id)
    {
        var resultado = await _mediator.Send(new EliminarPrendaCommand { Id = id });
        return Ok(resultado);
    }
}
