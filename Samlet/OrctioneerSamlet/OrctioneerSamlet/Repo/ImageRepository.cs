using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareDatabase.DBContext;
using VareDatabase.Interfaces;
using VareDatabase.Models;

namespace VareDatabase.Repo
{
    public class ImageRepository : Repository<ImageEntity>, IImageRepository
    {
        private VareDataModelContext db;

        public ImageRepository(VareDataModelContext db) : base(db)
        {
            this.db = db;
        }

        public void AddImage(int a, string b)
        {

        }

        public void Delete()
        {

        }

        public void DeleteImage(int a, int b)
        {

        }
        public override void Delete(ImageEntity image)
        {

        }
        
    }
}
