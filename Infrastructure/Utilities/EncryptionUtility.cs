using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities
{
    public class EncryptionUtility(IOptions<Configs> appConfig)
    {
        private readonly Configs _appConfig = appConfig.Value;

        public string GetSHA256(string password, string salt)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password + salt));
            string hash = Convert.ToHexStringLower(bytes);

            return hash;
        }

        public string GetNewSalt()
        {
            return Guid.NewGuid().ToString();
        }

        public (string, int) GetNewToken(Guid userId)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.UTF8.GetBytes(_appConfig.TokenKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new("userId", userId.ToString()),
                    new("TimeOut-Minute", _appConfig.TokenTimeout.ToString()),
                ]),

                Expires = DateTime.UtcNow.AddMinutes(_appConfig.TokenTimeout),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return (tokenHandler.WriteToken(token), _appConfig.TokenTimeout);
        }
    }
}
