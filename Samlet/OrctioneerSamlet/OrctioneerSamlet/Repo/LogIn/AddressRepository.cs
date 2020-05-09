using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class AddressRepository : Repository<AddressEntity>, IAddressRepository
    {
        private WalletContext db;

        public AddressRepository(WalletContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<AddressEntity> GetAddress(string id)
        {
            var address = await (from i in db.Users
                where i.userID == id
                select i.Address).FirstOrDefaultAsync();
            return address;
        }

        public async Task<int> addAddress(AddressEntity address, string id)
        {
            var user = (from i in db.Users
                where i.userID == id
                select i).FirstOrDefault();
            user.Address = address;
            db.Update(user);
            return await db.SaveChangesAsync();
        }

        public async Task<int> updateAddress(AddressEntity address, string id)
        {
            var user = (from i in db.Users
                where i.userID == id
                select i).FirstOrDefault();
            user.Address = address;
            db.Update(user);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeleteAddress(string id)
        {
            var user = (from i in db.Users
                where i.userID == id
                select i).FirstOrDefault();
            user.Address = null;
            db.Update(user);
            return await db.SaveChangesAsync();
        }
    }
}