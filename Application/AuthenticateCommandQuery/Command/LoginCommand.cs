using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Utilities;
using MediatR;

namespace Application.AuthenticateCommandQuery.Command
{
    public class LoginCommand : IRequest<LoginCommandResponse>
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

    public class LoginCommandHandler(IUserRepository repository, IMapper mapper, EncryptionUtility encryptionUtility) : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _repository.GetUserByUserName(request.UserName);
            if (user is not null)
            {
                string hashPassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
                if (hashPassword == user.Password)
                {
                    LoginCommandResponse response = _mapper.Map<LoginCommandResponse>(user);
                    (response.Token, response.TokenExpireTime) = _encryptionUtility.GetNewToken(user.Id);
                    (response.RefreshToken, response.RefreshTokenExpireTime) = _encryptionUtility.GetNewRefreshToken();

                    return response;
                }
            }

            throw new InvalidDataException();
        }
    }
}
