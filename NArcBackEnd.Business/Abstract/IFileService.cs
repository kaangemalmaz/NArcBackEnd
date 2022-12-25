using Microsoft.AspNetCore.Http;

namespace NArcBackEnd.Business.Abstract
{
    public interface IFileService
    {
        string FileSaveToServer(IFormFile file, string filePath);
        string FileSaveToFtp(IFormFile file);
    }
}
