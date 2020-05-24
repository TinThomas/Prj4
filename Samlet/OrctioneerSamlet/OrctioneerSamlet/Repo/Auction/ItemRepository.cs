using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VareDatabase.Models;
using VareDatabase.DBContext;
using VareDatabase.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Repo.Auction
{
    public class ItemRepository : Repository<ItemEntity>, IItemRepository
    {
        private VareDataModelContext db;
        public ItemRepository(VareDataModelContext db) : base(db)
        {
            this.db = db;
        }
        public override ItemEntity Get(int id)
        {
            return Context.Set<ItemEntity>()
                .Where(x => x.ItemId == id && x.Sold != true)
                .Include(tag => tag.Tags)
                .Include(bid => bid.Bids)
                .First();
        }
        public override IEnumerable<ItemEntity> Get()
        {
            return Context.Set<ItemEntity>()
                .Include(tag => tag.Tags)
                .Include(bid => bid.Bids)
                .Where(item => item.Sold != true).ToList();
        }
        public void GenerateTags(ItemEntity item)
        {
            string[] words = item.Title.Split(' ', ',', '.');
            foreach (string s in words)
            {
                if (s != null)
                {
                    AddTag(item, s);
                }
            }
        }
        public void AddTag(ItemEntity item, string newTag)
        {
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
            i.Sold = true;
            Update(i);
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

        public IEnumerable<ItemEntity> GetNewestFirst()
        {
            return Context.Set<ItemEntity>()
                .Include(tag => tag.Tags)
                .Include(bid => bid.Bids)
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }

        public IEnumerable<ItemEntity> GetMostPopularItems()
        {
            return Context.Set<ItemEntity>()
                .Include(tag => tag.Tags)
                .Include(bid => bid.Bids)
                .OrderByDescending(i => i.Bids.Count)
                .ToList();
        }

        public IEnumerable<ItemEntity> GetExpiringFirst()
        {
            return Context.Set<ItemEntity>()
                .Include(tag => tag.Tags)
                .Include(bid => bid.Bids)
                .OrderBy(i => i.ExpirationDate)
                .ToList();
        }

        public async Task<string> UploadPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                string imgFolder = @"clientapp\src\images";
                string path = Path.Combine(imgFolder, file.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                string newFileName = Guid.NewGuid().ToString() + ".jpg";
                File.Move(path, Path.Combine(imgFolder, newFileName));
                return newFileName;
            }
            return null;
        }
    }
}
