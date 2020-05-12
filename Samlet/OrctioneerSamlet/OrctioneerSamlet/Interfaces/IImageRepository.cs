using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VareDatabase.Models;

namespace VareDatabase.Interfaces
{
    public interface IImageRepository : IRepository<ImageEntity>
    {
        void UploadPicture(IFormFile file);
    }
}
