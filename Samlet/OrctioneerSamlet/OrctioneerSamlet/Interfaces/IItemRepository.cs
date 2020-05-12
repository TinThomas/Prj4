using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareDatabase.Models;

namespace VareDatabase.Interfaces
{
    public interface IItemRepository : IRepository<ItemEntity>
    {
        ItemEntity GenerateItem(string title, string description, string[] tags, string[] images, int expire, int userId, int buyOut);
        IEnumerable<ItemEntity> Search(string searchString);
        IEnumerable<ItemEntity> GetNewestFirst();
        IEnumerable<ItemEntity> GetMostPopularItems();
        IEnumerable<ItemEntity> GetExpiringFirst();

        IEnumerable<ItemEntity> getAllOnItem(int id);
        void GenerateTags(ItemEntity item);
        void AddTag(int id, string newTag);
    }
}
