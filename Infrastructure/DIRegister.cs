using Core.IRepositories;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIRegister
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            _ = services.AddScoped<IProductRepository, ProductRepository>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            _ = services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            _ = services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddInfraUtility(this IServiceCollection services)
        {
            _ = services.AddSingleton<EncryptionUtility>();
            _ = services.AddSingleton<CacheUtility>();
            _ = services.AddSingleton<FileUtility>();
        }
    }
}
