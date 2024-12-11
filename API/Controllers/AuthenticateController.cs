using Application.AuthenticateCommandQuery.Command;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [AllowAnonymous]
    public class AuthenticateController(IMediator mediator, ILogger<ProductController> logger) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpPost("Login")]
        [SwaggerOperation(
        Summary = "Login a User",
        Description = "Login a User with LoginCommand",
        OperationId = "Authenticate.Login",
        Tags = ["Authenticate"])]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            Response<LoginCommandResponse> result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Register")]
        [SwaggerOperation(
        Summary = "Register a User",
        Description = "Register a User with RegisterCommand",
        OperationId = "Authenticate.Register",
        Tags = ["Authenticate"])]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            Response<RegisterCommandResponse> result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("GenerateToken")]
        [SwaggerOperation(
        Summary = "Generate Token",
        Description = "Generate Token with GenerateTokenCommand",
        OperationId = "Authenticate.GenerateToken",
        Tags = ["Authenticate"])]
        public async Task<IActionResult> GenerateToken(GenerateTokenCommand command)
        {
            Response<GenerateTokenCommandResponse> result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
