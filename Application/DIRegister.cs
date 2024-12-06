using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIRegister
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            _ = services.AddScoped<IPermissionService, PermissionService>();
        }
    }
}
