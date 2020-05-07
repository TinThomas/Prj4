using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class UsernameRepository : Repository<UsernameEntity>, IUsernameRepository
    {
        private UserModelContext db;

        public UsernameRepository(UserModelContext _db) : base(_db)
        {
            db = _db;
        }

        public async Task<string> addUser(UsernameEntity user)
        {
            Console.WriteLine("seeing user:" + user.Username);
            string id = Guid.NewGuid().ToString();
            user.UserId = id;
            db.AddAsync(user);
            var done = await db.SaveChangesAsync();
            return id;

        }


        public async Task<string> validateUsername(string username)
        {
            string query = (from i in db.Users
                where i.Username == username
                select i.UserId).FirstOrDefault();
            return query;
        }

        public async Task<string> validateEmail(string email)
        {
            string query = (from i in db.Users
                where i.Email == email
                select i.UserId).FirstOrDefault();
            return query;
        }

        public async void updateUsername(UsernameEntity user)
        {
           var query = (from i in db.Users
                where i.UserId == user.UserId
                      select i).FirstOrDefault();
           query.Username = user.Username;
           db.Update(query);
           int wait = await db.SaveChangesAsync();

        }

        public void updateEmail(UsernameEntity user)
        {
            var query = (from i in db.Users
                where i.UserId == user.UserId
                select i).FirstOrDefault();
            query.Email = user.Email;
            db.Update(query);
            db.SaveChangesAsync();
        }

        public async Task<bool> CheckUser(UsernameEntity user)
        {
            var query = (from i in db.Users
                where i.Username == user.Username
                select i).FirstOrDefault();
            if (query != null)
            {
                return false;
            }
            query = (from i in db.Users
                where i.Email == user.Email
                select i).FirstOrDefault();
            if (query != null)
            { 
                return false;
            }

            return true;
        }

        public void DeleteUser(string id)
        {
            var query = (from i in db.Users
                where i.UserId == id
                select i).FirstOrDefault();
            db.Remove(query);
            db.SaveChangesAsync();
        }


    }
}