using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Pedidos.Commands.CrearPedido;
using MiApp.Application.Features.Pedidos.Queries.ObtenerPedidoPorId;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PedidosController : ControllerBase
{
    private readonly IMediator _mediator;

    public PedidosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CrearPedidoResponse>> Crear([FromBody] CrearPedidoCommand command)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        command.UsuarioId = userId;

        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.PedidoId }, resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerPedidoPorIdResponse>> ObtenerPorId(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new ObtenerPedidoPorIdQuery { Id = id, UsuarioId = userId };

        var resultado = await _mediator.Send(query);
        return Ok(resultado);
    }
}
