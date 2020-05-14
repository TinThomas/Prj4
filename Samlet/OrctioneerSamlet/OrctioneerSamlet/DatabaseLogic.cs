using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VareDatabase.Interfaces;
using VareDatabase.Repo;
using VareDatabase.Repo.Auction;
using VareDatabase.Models;
using VareDatabase.Interfaces.Auction;

namespace VareDatabase
{
    public class DatabaseLogic
    {
        private readonly IUnitOfWork unit;
        private readonly IItemRepository itemRepo;
        private readonly IBidRepository bidRepo;
        private readonly ITagRepository tagRepo;
        public DatabaseLogic(IUnitOfWork unit, IItemRepository itemRepo = null, IBidRepository bidRepo = null, ITagRepository tagRepo = null)
        {
            this.unit = unit;
            this.itemRepo = itemRepo;
            this.bidRepo = bidRepo;
            this.tagRepo = tagRepo;
        }
        public void CreateBid(BidEntity bid)
        {
            var item = itemRepo.Read(bid.ItemId);
            if(item.Bids.Last().Bid < bid.Bid) //check if new bid is high enough
            {
                bid.Created = DateTime.Now;
                bidRepo.Create(bid);
            }
            //error handling here
        }
        public IEnumerable<BidEntity> GetBidsFromItem(int itemId)
        {
            return bidRepo.GetBidsFromItem(itemId);
        }

        public IEnumerable<BidEntity> GetBidsByUserId(string userId)
        {
            return bidRepo.GetBidsByUser(userId);
        }

        public IEnumerable<BidEntity> GetBidsForItemSorted(int itemId)
        {
            return bidRepo.GethighestBidOnItem(itemId);
        }

        public IEnumerable<ItemEntity> GetNewestFirst()
        {
            return itemRepo.GetNewestFirst();
        }

        public IEnumerable<ItemEntity> GetMostPopularItems()
        {
            return itemRepo.GetMostPopularItems();
        }

        public IEnumerable<ItemEntity> GetExpiringFirst()
        {
            return itemRepo.GetExpiringFirst();
        }
        public void AddItem(ItemEntity item)
        {
            item.DateCreated = DateTime.Now;
            itemRepo.Create(item);
            itemRepo.GenerateTags(item);
            unit.Commit();
        }
        public void Delete(ItemEntity item)
        {
            itemRepo.Delete(item);
        }

        public IEnumerable<ItemEntity> GetAll()
        {
            return itemRepo.GetAll();
        }

        public ItemEntity Get(int id)
        {
            return itemRepo.Read(id);
        }
        public ItemEntity GetSingle(int id)
        {
            return itemRepo.Read(id);
        }
        public int Save()
        {
            return unit.Commit();
        }
        public void AddTag(int id, string newTag)
        {
            var item = itemRepo.Read(id);
            itemRepo.AddTag(item, newTag);
        }
        public IEnumerable<ItemEntity> Search(string searchingstring)
        {
            return itemRepo.Search(searchingstring);
        }
        public async Task<string> UploadPicture(IFormFile file)
        {
           string  path = await itemRepo.UploadPicture(file);
           return path;
        }
    }
}
