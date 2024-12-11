using Application.AuthenticateCommandQuery.Notification;
using AutoMapper;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticateCommandQuery.Command
{
    public class LoginCommand : IRequest<Response<LoginCommandResponse>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int TokenExpireTime { get; set; }
        public int RefreshTokenExpireTime { get; set; }
    }

    public class LoginCommandHandler(IUserRepository repository, IMapper mapper, EncryptionUtility encryptionUtility, IMediator mediator, ILogger<LoginCommandHandler> logger) : IRequestHandler<LoginCommand, Response<LoginCommandResponse>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<LoginCommandHandler> _logger = logger;

        public async Task<Response<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            LoginCommandResponse? response = null;
            try
            {
                User? user = await _repository.GetUserByUserName(request.UserName);
                if (user is not null)
                {
                    string hashPassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
                    if (hashPassword == user.Password)
                    {
                        response = _mapper.Map<LoginCommandResponse>(user);
                        (response.Token, response.TokenExpireTime) = _encryptionUtility.GetNewToken(user.Id);
                        (response.RefreshToken, response.RefreshTokenExpireTime) = _encryptionUtility.GetNewRefreshToken();
                        AddRefreshTokenNotification addRefreshTokenNotification = new()
                        {
                            RefreshToken = response.RefreshToken,
                            RefreshTokenTimeout = response.RefreshTokenExpireTime,
                            UserId = user.Id,
                        };
                        await _mediator.Publish(addRefreshTokenNotification, cancellationToken);
                        return Response.Result(response, ResponseMessage.Success);
                    }
                }

                return Response.Result(response, ResponseMessage.WrongUsernameOrPassword, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                string message = $"In {nameof(LoginCommandHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.Result(response, message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
