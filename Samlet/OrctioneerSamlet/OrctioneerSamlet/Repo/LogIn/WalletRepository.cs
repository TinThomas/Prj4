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
                join c in db.Cards on i.card.id equals c.id
                where i.userID == id
                select new WalletEntity()
                {
                    Amount = i.Amount,
                    card = c,
                    id = i.id,
                    userID = i.userID
                }).FirstOrDefaultAsync();
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
            if (query == null)
            {
                return 0;
            }
            query.card = wallet.card;
            query.Amount += query.Amount;
            db.Update(query);
            return await db.SaveChangesAsync();
        }


        public async Task<int> DeleteCard(string id)
        {
            var query = await (from i in db.Wallets
                join c in db.Cards on i.card.id equals c.id
                where i.userID == id
                select new WalletEntity()
                {
                    Amount = i.Amount,
                    card = c,
                    id = i.id,
                    userID = i.userID
                }).FirstOrDefaultAsync();
            if (query != null)
            {
                db.Cards.Remove(query.card);
                query.card = null;
                db.Update(query);
            }
            return await db.SaveChangesAsync();
        }

    }
}