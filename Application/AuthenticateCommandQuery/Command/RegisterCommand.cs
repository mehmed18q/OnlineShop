using AutoMapper;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticateCommandQuery.Command
{
    public class RegisterCommand : IRequest<Response<RegisterCommandResponse>>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
    public class RegisterCommandResponse
    {
        public required string UserName { get; set; }
    }

    public class RegisterCommandHandler(IUserRepository repository, IMapper mapper, IUnitOfWork unitOfWork, EncryptionUtility encryptionUtility, ILogger<RegisterCommandHandler> logger) : IRequestHandler<RegisterCommand, Response<RegisterCommandResponse>>
    {
        private readonly IUserRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly EncryptionUtility _encryptionUtility = encryptionUtility;
        private readonly ILogger<RegisterCommandHandler> _logger = logger;
        public async Task<Response<RegisterCommandResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            RegisterCommandResponse response = new()
            {
                UserName = request.UserName,
            };

            try
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
                    return Response.Result(response, ResponseMessage.AccountCreated);
                }
                else
                {
                    return Response.Result(response, ResponseMessage.DuplicateUsername, System.Net.HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                string message = $"In {nameof(RegisterCommandHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
                return Response.Result(response, message, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
