using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace CamlifeAPI1.Class.Documents
{
    public class DocumentUpload
    {
        public string Upload(IFormFile file)
        {
            //extention
            List<string> validExt = new List<string>() {".jpg",".png",".gif",".pdf" };
            string ext = Path.GetExtension(file.FileName);
            if(!validExt.Contains(ext)) { 
            return "Extension is not valid("+string.Join(", ", validExt)+")";
             //  return $"Extension is not valid ({ string.Join(",",validExt) });
            }
            //file zise 
            long size = file.Length;
            if (size > (5 * 1024 * 1024))
            {
                return "Maximum size can be 5MB.";
            }
            //name checking
            string fileName = Guid.NewGuid().ToString() + ext;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "UploadFiles");
            FileStream stream = new FileStream(path + fileName, FileMode.Create);
                 file.CopyTo(stream);
            stream.Dispose();
            stream.Close();
          
          
            return fileName;
        }
    }
}