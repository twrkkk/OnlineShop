using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public static class Image
    {
        public static bool Upload(IFormFile file, string folderPath, out string fileName)
        {
            fileName = string.Empty;

            if (file != null)
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                using (var fileStream = new FileStream(folderPath + fileName, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return fileName != string.Empty;    
        }
    }
}
