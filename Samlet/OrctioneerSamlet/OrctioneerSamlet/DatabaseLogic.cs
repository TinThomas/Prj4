﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public DatabaseLogic(IUnitOfWork unit, IItemRepository itemRepo = null, IBidRepository bidRepo = null)
        {
            this.unit = unit;
            this.itemRepo = itemRepo;
            this.bidRepo = bidRepo;
        }
        public void CreateBid(BidEntity bid)
        {
            bidRepo.AddBid(bid.ItemId,  bid.Bid,  bid.UserIdBuyer);
            unit.Commit();
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
        public IEnumerable<ItemEntity> Search(string searchingstring)
        {
            return itemRepo.Search(searchingstring);
        }
    }
}
