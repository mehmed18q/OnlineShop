using AutoMapper;
using Core.Entities.Security;
using Core.IRepositories;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.AuthenticateCommandQuery.Notification
{
    public class AddRefreshTokenNotification : INotification
    {
        public string RefreshToken { get; set; } = null!;
        public Guid UserId { get; set; }
        public int RefreshTokenTimeout { get; set; }
    }

    public class AddRefreshTokenNotificationHandler(IUserRefreshTokenRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddRefreshTokenNotificationHandler> logger) : INotificationHandler<AddRefreshTokenNotification>
    {
        private readonly IUserRefreshTokenRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<AddRefreshTokenNotificationHandler> _logger = logger;
        public async Task Handle(AddRefreshTokenNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                UserRefreshToken userRefreshToken = _mapper.Map<UserRefreshToken>(notification);
                UserRefreshToken? currentRefreshToken = await _repository.GetCurrentUserRefreshToken(notification.UserId);
                if (currentRefreshToken is null)
                {
                    await _repository.InsertAsync(userRefreshToken);
                }
                else
                {
                    currentRefreshToken.RefreshToken = userRefreshToken.RefreshToken;
                    currentRefreshToken.RefreshTokenTimeout = userRefreshToken.RefreshTokenTimeout;
                    currentRefreshToken.CreateDate = userRefreshToken.CreateDate;
                    currentRefreshToken.IsValid = true;
                    await _repository.Update(currentRefreshToken);
                }
                _ = await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {

                string message = $"In {nameof(AddRefreshTokenNotificationHandler)}: Error Message: {e.Message}. Exception: {e.InnerException}";
                _logger.LogError(message);
            }
        }
    }
}
