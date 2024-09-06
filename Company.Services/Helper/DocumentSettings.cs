using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string FolderName)
        {
            //1.Get FolderPath
            //var FilePath = @"E:\Route\Route practical\MVC\Demos\Company.web\wwwroot\File\Images\";

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File", FolderName);

            //2. Get FilePath
            //Store In DataBase
            var FileName = $"{Guid.NewGuid()}-{file.FileName}";

            //3.Combine FolderPath + FilePath
            var filepath = Path.Combine(FolderPath, FileName);

            //4.Save File
            using var fileStream = new FileStream(filepath,FileMode.Create);

            file.CopyTo(fileStream);

            return FileName;
        }
    }
}
