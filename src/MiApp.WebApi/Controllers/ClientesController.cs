using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Clientes.Commands.RegistrarCliente;
using MiApp.Application.Features.Clientes.Queries.ObtenerClientePorId;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene un cliente por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerClientePorIdResponse>> ObtenerPorId(int id)
    {
        var resultado = await _mediator.Send(new ObtenerClientePorIdQuery { Id = id });
        return Ok(resultado);
    }

    /// <summary>
    /// Registra un nuevo cliente.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RegistrarClienteResponse>> Registrar([FromBody] RegistrarClienteCommand command)
    {
        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.ClienteId }, resultado);
    }
}
