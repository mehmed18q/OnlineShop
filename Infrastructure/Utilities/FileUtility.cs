using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Infrastructure.Utilities
{
    public class FileUtility(IHostEnvironment environment, IConfiguration configuration, IHttpContextAccessor httpContext, IOptions<Configs> config)
    {
        private readonly IHostEnvironment _environment = environment;
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor _httpContext = httpContext;
        private readonly Configs _config = config.Value;

        public string? UploadFile(IFormFile? file, string entityRootPath)
        {
            if (file is not null)
            {
                string applicationExecutionRootPath = _environment.ContentRootPath;
                string mediaRootPath = _configuration.GetValue<string>("MediaPath")!;
                string fileName = CreateFileName(file);
                string newFilePath = Path.Combine(CheckAndCreateDirectory(
                [
                    applicationExecutionRootPath,
                    mediaRootPath,
                    entityRootPath
                ]), fileName);
                using BinaryWriter writer = new(File.OpenWrite(newFilePath));
                byte[] byteArray = ConvertToByteArray(file)!;
                byteArray = EncryptFile(byteArray);
                writer.Write(byteArray);
                return fileName;
            }

            return default;
        }

        public static (byte[]? bytes, string? fileName, string? extension, long? size) GetProductThumbnailInfo(IFormFile? file)
        {
            return file is not null ? (ConvertToByteArray(file), file.FileName, GetFileExtension(file), file.Length) : default;
        }

        public string? ConvertToBase64(byte[]? fileArray)
        {
            return fileArray is not null ? Convert.ToBase64String(fileArray) : default;
        }

        public string? GetFileUrl(string? fileName, string entityRootPath)
        {
            string hostUrl = _httpContext.HttpContext.Request.Host.Value;
            string httpMode = $"http{(_httpContext.HttpContext.Request.IsHttps ? "s" : "")}";
            return $"{httpMode}://{hostUrl}/Media/{entityRootPath}/{fileName}";
        }

        public string? GetFilePath(string? fileName, string? entityRootPath)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(entityRootPath))
            {
                string applicationExecutionRootPath = _environment.ContentRootPath;
                string mediaRootPath = _configuration.GetValue<string>("MediaPath")!;
                return Path.Combine(applicationExecutionRootPath, mediaRootPath, entityRootPath, fileName);
            }
            return default;
        }

        #region Privates
        public byte[] EncryptFile(byte[] fileContent)
        {
            using Aes encryptor = GetEncryptor();

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(fileContent, 0, fileContent.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        public byte[] DecryptFile(byte[] fileContent)
        {
            using Aes encryptor = GetEncryptor();

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(fileContent, 0, fileContent.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        private Aes GetEncryptor()
        {
            Aes encryptor = Aes.Create();
            byte[] salt = "Ivan Medvedev"u8.ToArray();
            Rfc2898DeriveBytes rdb = new(_config.FileEncryptionKey, salt);
            encryptor.Key = rdb.GetBytes(32);
            encryptor.IV = rdb.GetBytes(16);
            return encryptor;
        }

        private static byte[]? ConvertToByteArray(IFormFile? file)
        {
            if (file is not null)
            {
                using MemoryStream memoryStream = new();
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }

            return default;
        }

        private static string? GetFileExtension(IFormFile? file)
        {
            if (file is not null)
            {
                FileInfo fileInfo = new(file.FileName);
                return fileInfo.Extension;
            }

            return default;
        }

        private static string CreateFileName(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}{GetFileExtension(file)}";
            return fileName;
        }

        private static string CheckAndCreateDirectory(List<string> paths)
        {
            string PerviousPath = paths[0];
            for (int i = 1; i < paths.Count; i++)
            {
                string newPath = Path.Combine(PerviousPath, paths[i]);
                if (!Directory.Exists(newPath))
                {
                    _ = Directory.CreateDirectory(newPath);
                }
                PerviousPath = newPath;
            }

            return PerviousPath;
        }

        #endregion
    }
}
