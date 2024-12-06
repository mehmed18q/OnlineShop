using Application.AuthenticateCommandQuery.Notification;
using AutoMapper;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using MediatR;

namespace Application.AuthenticateCommandQuery.Command
{
    public class GenerateTokenCommand : IRequest<GenerateTokenCommandResponse>
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }
    public class GenerateTokenCommandResponse
    {
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
    }

    public class GenerateTokenCommandHandler(IUserRefreshTokenRepository repository, IMapper mapper, IMediator mediator, IUnitOfWork unitOfWork, EncryptionUtility encryptionUtility) : IRequestHandler<GenerateTokenCommand, GenerateTokenCommandResponse>
    {
        private readonly IUserRefreshTokenRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMediator _mediator = mediator;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;

        public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
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

                return new GenerateTokenCommandResponse { RefreshToken = addRefreshTokenNotification.RefreshToken, Token = token };
            }

            throw new Exception();
        }
    }
}
