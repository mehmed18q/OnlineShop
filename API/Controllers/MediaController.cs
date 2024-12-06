using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    public class MediaController(FileUtility fileUtility) : BaseController
    {
        private readonly FileUtility _fileUtility = fileUtility;

        [HttpGet("{entityRootPath}/{fileUrl}")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Get File Content",
            Description = "Get Decrypted File with File Url",
            OperationId = "File.GetFileContent",
            Tags = ["File"])
        ]
        public async Task<IActionResult> GetFileContent(string entityRootPath, string fileUrl)
        {
            string? filePath = _fileUtility.GetFilePath(fileUrl, entityRootPath);
            if (filePath is not null)
            {
                byte[] encryptedData = await System.IO.File.ReadAllBytesAsync(filePath);
                byte[] decryptedData = _fileUtility.DecryptFile(encryptedData);

                return new FileContentResult(decryptedData, "application/txt");
            }

            return NotFound();
        }
    }
}
