using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VareDatabase.DBContext;
using VareDatabase.Repo;
using VareDatabase.Models;


namespace VareDatabase.Interfaces.Auction
{
    public interface IBidRepository : IRepository<BidEntity>
    {
        IEnumerable<BidEntity> GetBidsFromItem(int itemId);
        IEnumerable<BidEntity> GetBidsByUser(int userId);
        void AddBid(int itemId, int bid, int user);
    }
}
