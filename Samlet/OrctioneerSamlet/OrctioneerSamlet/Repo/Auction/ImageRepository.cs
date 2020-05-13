using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VareDatabase.DBContext;
using VareDatabase.Interfaces;
using VareDatabase.Models;
using OkResult = System.Web.Http.Results.OkResult;

namespace VareDatabase.Repo
{
    public class ImageRepository : Repository<ImageEntity>, IImageRepository
    {
        private VareDataModelContext db;

        public ImageRepository(VareDataModelContext db) : base(db)
        {
            this.db = db;
        }
        public string UploadPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                string imgFolder = @"..\images";
                string path = Path.Combine(imgFolder, file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }
                string newFileName = Guid.NewGuid().ToString()+".jpg";
                File.Move(path, Path.Combine(imgFolder, newFileName));
                return Path.Combine(imgFolder, newFileName);
            }
            return null;
        }
    }
}
