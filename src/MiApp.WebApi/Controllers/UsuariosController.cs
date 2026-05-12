using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Features.Usuarios.Commands.CrearUsuario;
using MiApp.Application.Features.Usuarios.Queries.ObtenerUsuarioPorId;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ObtenerUsuarioPorIdResponse>> ObtenerPorId(int id)
    {
        try
        {
            var resultado = await _mediator.Send(new ObtenerUsuarioPorIdQuery { Id = id });
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return NotFound(new { mensaje = ex.Message });
        }
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CrearUsuarioResponse>> Crear([FromBody] CrearUsuarioCommand command)
    {
        var resultado = await _mediator.Send(command);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = resultado.UsuarioId }, resultado);
    }
}
