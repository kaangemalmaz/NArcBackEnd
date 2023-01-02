using Microsoft.AspNetCore.Http;

namespace NArcBackEnd.Business.Utilities.File
{
    public interface IFileService
    {
        string FileSaveToServer(IFormFile file, string filePath);
        string FileSaveToFtp(IFormFile file);
        void FileDeleteToServer(string path);
        void FileDeleteToFtp(string path);
    }
}
