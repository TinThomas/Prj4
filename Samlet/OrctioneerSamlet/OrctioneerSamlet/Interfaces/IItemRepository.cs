using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VareDatabase.Models;

namespace VareDatabase.Interfaces
{
    public interface IItemRepository : IRepository<ItemEntity>
    {
        IEnumerable<ItemEntity> Search(string searchString);
        IEnumerable<ItemEntity> GetNewestFirst();
        IEnumerable<ItemEntity> GetMostPopularItems();
        IEnumerable<ItemEntity> GetExpiringFirst();
        string UploadPicture (IFormFile file);
        void GenerateTags(ItemEntity item);
        void AddTag(int id, string newTag);
    }
}
