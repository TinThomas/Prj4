using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareDatabase.Interfaces;
using VareDatabase.DBContext;
using Microsoft.EntityFrameworkCore;
using VareDatabase.Interfaces.Auction;
using VareDatabase.Models;
using VareDatabase.Repo.Auction;

namespace VareDatabase.Repo
{
    public class AuctionUnitOfWork : IUnitOfWork
    {
        public VareDataModelContext Context { get; protected set; }
        private ItemRepository itemRepository;
        private TagRepository tagRepository;
        private BidRepository bidRepository;
        public AuctionUnitOfWork()
        {
            Context = new VareDataModelContext();
        }
        public IItemRepository ItemRepository
        {
            get
            {

                if (this.itemRepository == null)
                {
                    this.itemRepository = new ItemRepository(Context);
                }
                return itemRepository;
            }
        }
        public ITagRepository TagRepository
        {
            get
            {

                if (this.tagRepository == null)
                {
                    this.tagRepository = new TagRepository(Context);
                }
                return tagRepository;
            }
        }
        public IBidRepository BidRepository
        {
            get
            {

                if (this.bidRepository == null)
                {
                    this.bidRepository = new BidRepository(Context);
                }
                return bidRepository;
            }
        }
        public int Commit()
        {
            return Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
