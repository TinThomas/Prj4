using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareDatabase.Models;
using VareDatabase.DBContext;
using VareDatabase.Interfaces;

namespace VareDatabase.Repo.Auction
{
    public class ItemRepository : Repository<ItemEntity>, IItemRepository
    {
        private VareDataModelContext db;
        public ItemRepository(VareDataModelContext db) : base(db)
        {
            this.db = db;
        }
        public void GenerateTags(ItemEntity item)
        {
            string[] words = item.Title.Split(' ', ',', '.');
            foreach (string s in words)
            {
                if (s != null)
                {
                    AddTag(item.ItemId, s);
                }
            }
        }
        public void AddTag(int itemId, string newTag)
        {
            ItemEntity item = db.Set<ItemEntity>().ToList().FirstOrDefault(x => x.ItemId == itemId);
            if (item == null)
            {
                return;
            }
            bool exists = false;
            foreach (TagEntity t in item.Tags) //check each tag if it exists
            {
                if (t.Type == newTag)
                {
                    exists = true;
                }
            }
            if (!exists) //if it doesnt exist add it
            {
                item.Tags.Add(new TagEntity()
                {
                    Type = newTag,
                });
            }
        }
        public override void Delete(ItemEntity i)
        {
            var items = db.Set<ItemEntity>().ToList();
            ItemEntity itemToDelete = items.First(x => x.ItemId == i.ItemId);
            itemToDelete.Sold = true;
        }
        public IEnumerable<ItemEntity> Search(string title)
        {
            string[] search = title.Split(' ', ',', '.');
            List<ItemEntity> foundItems = new List<ItemEntity>();
            foreach (string s in search)
            {
                List<ItemEntity> ids = new List<ItemEntity>();
                ids = SearchByTag(s);
                foreach (ItemEntity i in ids)
                {
                    if (!foundItems.Contains(i))
                    {
                        foundItems.Add(i);
                    }
                }
            }
            return foundItems;
        }
        public ItemEntity GenerateItem(string title, string description, string[] tags, string[] images, int userId, int expire, int buyOut = -1)
        {
            List<ImageEntity> newImages = new List<ImageEntity>();
            for (int i = 0; i < images.Length; i++)
            {
                ImageEntity item = new ImageEntity();
                item.ImageOfItem = images[i];
                newImages[i] = item;
            }
            List<TagEntity> newTags = new List<TagEntity>();
            for (int i = 0; i < tags.Length; i++)
            {
                TagEntity tag = new TagEntity();
                tag.Type = tags[i];
                newTags[i] = tag;
            }
            ItemEntity itemEntity = new ItemEntity()
            {
                BuyOutPrice = buyOut,
                DateCreated = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(expire),
                Title = title,
                Images = newImages,
                Tags = newTags,
                DescriptionOfItem = description,
                UserIdSeller = userId,
            };
            GenerateTags(itemEntity);
            return itemEntity;
        }
        private List<ItemEntity> SearchByTag(string tag)
        {
            List<ItemEntity> itemsId = new List<ItemEntity>();
            var join = (from i in db.Items
                        join t in db.Tags on i.ItemId equals t.ItemId
                        select new
                        {
                            Title = i.Title,
                            Id = i.ItemId,
                            Tag = t.Type,
                        }).Where(x => x.Tag == tag).ToList();
            foreach (var j in join)
            {
                itemsId.AddRange(db.Items.Where(i => j.Id == i.ItemId).ToList());
            }
            return itemsId;
        }
    }
}
