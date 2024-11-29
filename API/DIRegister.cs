using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public static class DIRegister
    {
        public static IServiceCollection AddJWT(this IServiceCollection services)
        {
            ServiceProvider sp = services.BuildServiceProvider();
            Configs configs = sp.GetService<IOptions<Configs>>()!.Value;
            byte[] key = Encoding.UTF8.GetBytes(configs.TokenKey);

            _ = services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(configs.TokenTimeout),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
