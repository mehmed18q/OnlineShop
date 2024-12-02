using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            string type = "Bearer";
            string version = "v1";
            _ = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = "Online Shop",
                    Description = "A Sample Project With New Futures",
                    TermsOfService = new Uri("http://SadeqKiumarsi.ir"),
                    License = new OpenApiLicense
                    {
                        Name = "Sadeq Kiumarsi",
                        Url = new Uri("http://SadeqKiumarsi.ir")
                    }
                });

                OpenApiReference openApiReference = new()
                {
                    Id = type,
                    Type = ReferenceType.SecurityScheme
                };

                options.AddSecurityDefinition(type, new OpenApiSecurityScheme
                {
                    Reference = openApiReference,
                    Scheme = type,
                    Name = "Authorization",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = openApiReference
                        }, []
                    }
                });

                options.EnableAnnotations();
            });

            return services;
        }
    }
}
