using Microsoft.AspNetCore.Http;
using NArcBackEnd.Business.Abstract;
using System.Net;

namespace NArcBackEnd.Business.Concrete
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            //file name'i unique yapmak için quid kullanılıyor.
            string fileName = file.FileName;
            var fileFormat = (fileName.Substring(fileName.LastIndexOf('.'))).ToLower();
            fileName = Guid.NewGuid().ToString() + fileFormat;


            string path = filePath + fileName;
            using (var stream = System.IO.File.Create(path))
            {
                file.CopyTo(stream);
            }


            return fileName;
        }

        // ftp ye upload!
        public string FileSaveToFtp(IFormFile file)
        {
            string fileName = file.FileName;
            var fileFormat = (fileName.Substring(fileName.LastIndexOf('.'))).ToLower();
            fileName = Guid.NewGuid().ToString() + fileFormat;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("FTP Adres yazılacak" + fileName);
            request.Credentials = new NetworkCredential("Kullanici adi", "şifre");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream ftpStream = request.GetRequestStream())
            {
                file.CopyTo(ftpStream);
            }

            return fileName;
        }
    }
}
