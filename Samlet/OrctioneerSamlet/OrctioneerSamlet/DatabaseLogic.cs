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
        private readonly IImageRepository imageRepo;
        public DatabaseLogic(IUnitOfWork unit, IItemRepository itemRepo = null, IBidRepository bidRepo = null, ITagRepository tagRepo = null, IImageRepository imageRepo = null)
        {
            this.unit = unit;
            this.itemRepo = itemRepo;
            this.bidRepo = bidRepo;
            this.tagRepo = tagRepo;
            this.imageRepo = imageRepo;
        }
        public void CreateBid(BidEntity bid)
        {
            var item = itemRepo.Read(bid.ItemId);
            if(item.Bids.Last().Bid < bid.Bid) //check if new bid is high enough
            {
                bidRepo.Create(bid);
                unit.Commit();
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
            itemRepo.Create(item);
            //itemRepo.GenerateTags(item);
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
            itemRepo.AddTag(id, newTag);
        }
        public void AddImage(ImageEntity img)
        {
            imageRepo.Create(img);
        }
        public IEnumerable<ItemEntity> Search(string searchingstring)
        {
            return itemRepo.Search(searchingstring);
        }

        public string UploadPicture(IFormFile file)
        {
           string  path = itemRepo.UploadPicture(file);
           return path;
        }
    }
}
