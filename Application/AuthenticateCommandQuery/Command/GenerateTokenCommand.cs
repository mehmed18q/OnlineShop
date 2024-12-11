using Application.AuthenticateCommandQuery.Notification;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticateCommandQuery.Command
{
    public class GenerateTokenCommand : IRequest<Response<GenerateTokenCommandResponse>>
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
    public class GenerateTokenCommandResponse
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }

    public class GenerateTokenCommandHandler(IUserRefreshTokenRepository repository, IMediator mediator, EncryptionUtility encryptionUtility, ILogger<GenerateTokenCommandHandler> logger) : IRequestHandler<GenerateTokenCommand, Response<GenerateTokenCommandResponse>>
    {
        private readonly IUserRefreshTokenRepository _repository = repository;
        private readonly IMediator _mediator = mediator;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;
        private readonly ILogger<GenerateTokenCommandHandler> _logger = logger;
        public async Task<Response<GenerateTokenCommandResponse>> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            GenerateTokenCommandResponse? response = null;
            try
            {
                UserRefreshToken? userRefreshToken = await _repository.GetCurrentUserRefreshToken(request.RefreshToken);
                if (userRefreshToken is not null)
                {
                    AddRefreshTokenNotification addRefreshTokenNotification = new()
                    {
                        UserId = userRefreshToken.UserId,
                    };
                    (string token, _) = _encryptionUtility.GetNewRefreshToken();
                    (addRefreshTokenNotification.RefreshToken, addRefreshTokenNotification.RefreshTokenTimeout) = _encryptionUtility.GetNewRefreshToken();

                    await _mediator.Publish(addRefreshTokenNotification, cancellationToken);
                    response = new GenerateTokenCommandResponse { RefreshToken = addRefreshTokenNotification.RefreshToken, Token = token };
                    return Response.Result(response, ResponseMessage.Success);
                }
                return Response.Result(response, ResponseMessage.NotFound, System.Net.HttpStatusCode.NotFound);
            }
            catch (Exception e)
            {
                string message = $"In {nameof(GenerateTokenCommandHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.Result(response, message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
