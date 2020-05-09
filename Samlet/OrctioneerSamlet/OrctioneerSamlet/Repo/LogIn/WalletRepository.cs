using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class WalletRepository : Repository<WalletEntity>, IWalletRepository
    {
        private WalletContext db;
        public WalletRepository(WalletContext _db) : base(_db)
        {
            db = _db;
        }


        public int getAmount(string id)
        {
            int query = (from i in db.Wallets
                where i.userID == id
                select i.Amount).FirstOrDefault();
            return query;
        }

        public WalletEntity getDetails(string id)
        {
            return new WalletEntity();
        }

        public void addWallet(WalletEntity wallet)
        {

        }

        public void updateWallet(WalletEntity wallet)
        {

        }


        public void DeleteCard(string id)
        {

        }

    }
}