using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class PasswordRepository : Repository<PasswordEntity>, IPasswordRepository
    {
        private PassModelContext db;

        public PasswordRepository(PassModelContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<bool> validatePassword(PasswordEntity pass)
        {
            var query = await (from i in db.Passwords
                where i.UserId == pass.UserId
                select i).FirstOrDefaultAsync();
            if (query != null  && BCrypt.Net.BCrypt.Verify(pass.Password,query.Password))
            {
                return true;
            }

            return false;
        }

        public async Task<int> CreatePassword(PasswordEntity password)
        {
            db.Passwords.Add(password);
            return await db.SaveChangesAsync();
        }

        public async Task<int> updatePassword(PasswordEntity password)
        {
            var query = (from i in db.Passwords
                where i.UserId == password.UserId
                select i).FirstOrDefault();
            query.Password = password.Password;
            db.Passwords.Update(query);
            return await db.SaveChangesAsync();
        }

        public async Task<int> DeletePassword(string id)
        {
            var query = (from i in db.Passwords
                where i.UserId == id
                select i).FirstOrDefault();
            db.Remove(query);
            return await db.SaveChangesAsync();
        }
    }
}