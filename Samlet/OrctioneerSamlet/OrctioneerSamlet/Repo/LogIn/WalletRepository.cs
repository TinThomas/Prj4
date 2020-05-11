using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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


        public async Task<int> getAmount(string id)
        {
            return await (from i in db.Wallets
                where i.userID == id
                select i.Amount).FirstOrDefaultAsync();
        }

        public async Task<int> setAmount(string id, int amount)
        {
            var query = (from i in db.Wallets
                where i.userID == id
                select i).FirstOrDefault();
            query.Amount += amount;
            db.Update(query);
            return await db.SaveChangesAsync();
        }

        public async Task<WalletEntity> getDetails(string id)
        {
            return await (from i in db.Wallets
                where i.userID == id
                select i).FirstOrDefaultAsync();
        }

        public async Task<int> addWallet(WalletEntity wallet)
        {
            db.Add(wallet);
            return await db.SaveChangesAsync();
        }

        public async Task<int> updateWallet(WalletEntity wallet)
        {
            var query = (from i in db.Wallets
                where i.userID == wallet.userID
                select i).FirstOrDefault();
            query.card = wallet.card;
            query.Amount = query.Amount;
            db.Update(query);
            return await db.SaveChangesAsync();
        }


        public async Task<int> DeleteCard(string id)
        {
            var query = (from i in db.Wallets
                where i.userID == id
                select i).FirstOrDefault();
            if (query != null)
            {
                db.Remove(query);
            }
            return await db.SaveChangesAsync();
        }

    }
}