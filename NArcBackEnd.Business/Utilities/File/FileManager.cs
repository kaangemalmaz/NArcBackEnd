using Microsoft.AspNetCore.Http;
using System.Net;

namespace NArcBackEnd.Business.Utilities.File
{
    public class FileManager : IFileService
    {
        public string FileSaveToServer(IFormFile file, string filePath)
        {
            //file name'i unique yapmak için quid kullanılıyor.
            string fileName = file.FileName;
            var fileFormat = fileName.Substring(fileName.LastIndexOf('.')).ToLower();
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
            var fileFormat = fileName.Substring(fileName.LastIndexOf('.')).ToLower();
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

        public void FileDeleteToServer(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception e)
            {

            }
        }

        public void FileDeleteToFtp(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp adresi " + path);
                request.Credentials = new NetworkCredential("kullanici adi", "password");
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (Exception e)
            {

            }
        }
    }
}
