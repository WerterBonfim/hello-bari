using CQRS.Core.Infrastructure;
using Hello.Common.Commands;
using Hello.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Werter.HelloCmd.Api.Commands;
using Werter.HelloCmd.Api.DTOs;

namespace Werter.HelloCmd.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HelloController : ControllerBase
{
    private readonly ILogger<HelloController> _logger;
    private readonly ICommandDispatcher _commandDispatcher;

    public HelloController(ILogger<HelloController> logger, ICommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }
    
    [HttpPost]
    public async Task<IActionResult> SendHello(SendHelloCommand command)
    {
        var id = Guid.NewGuid();
        
        try
        {

            command.Id = id;
            await _commandDispatcher.SendAsync(command);
            return StatusCode(StatusCodes.Status201Created, new SendHelloResponse
            {
                Id = id,
                Message = "Nova mensagem enviada com sucesso!"
            });

        }
        catch (InvalidOperationException ex)
        {
            _logger.Log(LogLevel.Warning, ex, "Cliente recebeu um BadRequest!");
            return BadRequest(new BaseResponse
            {
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            const string safaErrorMessage = "Ocorreu um erro ao tentar processar a criação de um novo post!";
            _logger.Log(LogLevel.Error, ex, safaErrorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, new SendHelloResponse
            {
                Id = id,
                Message = safaErrorMessage
            });
        }
    }
}