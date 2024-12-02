using Application.AuthenticateCommandQuery.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("Login")]
        [SwaggerOperation(
        Summary = "Login a User",
        Description = "Login a User with LoginCommand",
        OperationId = "Authenticate.Login",
        Tags = ["Authenticate"])]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            LoginCommandResponse result = await _mediator.Send(command);
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
            RegisterCommandResponse result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
