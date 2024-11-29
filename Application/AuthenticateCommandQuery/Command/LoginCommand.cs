using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
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
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork, EncryptionUtility encryptionUtility) : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _repository.GetUserByUserName(request.UserName);
            if (user is not null)
            {
                string hashPassword = _encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
                if (hashPassword == user.Password)
                {
                    (string token, int expireTime) = _encryptionUtility.GetNewToken(user.Id);

                    LoginCommandResponse response = new()
                    {
                        UserName = user.UserName,
                        Token = token,
                        ExpireTime = expireTime,
                    };

                    return response;
                }
            }

            throw new InvalidDataException();
        }
    }
}
