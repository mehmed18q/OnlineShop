using AutoMapper;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using MediatR;

namespace Application.AuthenticateCommandQuery.Command
{
    public class RegisterCommand : IRequest<Unit>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class RegisterCommandHandler(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork, EncryptionUtility encryptionUtility) : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            string salt = _encryptionUtility.GetNewSalt();
            string hashPassword = _encryptionUtility.GetSHA256(request.Password, salt);

            User user = new()
            {
                Id = Guid.NewGuid(),
                Password = hashPassword,
                PasswordSalt = salt,
                UserName = request.UserName,
                RegisterDate = DateTime.Now
            };
            _ = await _repository.InsertAsync(user);
            _ = await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
