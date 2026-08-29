using Microsoft.AspNetCore.Mvc;

namespace CleanArchTemplate.Api.Controllers;

/// <summary>
/// Controller simples para validar se a API esta respondendo.
/// Use como modelo inicial para criar novos controllers HTTP.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public sealed class PingController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new PingResponse("pong", DateTime.UtcNow));
    }
}

/// <summary>
/// Corpo simples de retorno do endpoint de ping.
/// Em controllers reais, prefira retornar outputs vindos da camada Application.
/// </summary>
public sealed record PingResponse(string Message, DateTime Timestamp);
