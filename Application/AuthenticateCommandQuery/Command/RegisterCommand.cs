using AutoMapper;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using MediatR;

namespace Application.AuthenticateCommandQuery.Command
{
    public class RegisterCommand : IRequest<RegisterCommandResponse>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
    public class RegisterCommandResponse
    {
        public required string UserName { get; set; }
        public required string Message { get; set; }
    }

    public class RegisterCommandHandler(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork, EncryptionUtility encryptionUtility) : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            string salt = _encryptionUtility.GetNewSalt();
            string hashPassword = _encryptionUtility.GetSHA256(request.Password, salt);

            if (!await _repository.IsUserExist(request.UserName))
            {
                User user = _mapper.Map<User>(request);
                user.PasswordSalt = salt;
                user.Password = hashPassword;

                _ = await _repository.InsertAsync(user);
                _ = await _unitOfWork.SaveChangesAsync();

                return new RegisterCommandResponse
                {
                    UserName = request.UserName,
                    Message = "حساب کاربری با موفقیت ایجاد شد."
                };
            }

            return new RegisterCommandResponse
            {
                UserName = request.UserName,
                Message = "نام کاربری قابل قبول نمی باشد، نام کاربری دیگری انتخاب کنید."
            };
        }
    }
}
