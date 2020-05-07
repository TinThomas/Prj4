using System;
using System.Linq;
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

        public void addUser(UsernameEntity user)
        {
            string id = Guid.NewGuid().ToString();
            UsernameEntity _user = new UsernameEntity();
            user.UserId = id;
            user.Username = user.Username;
            user.Email = user.Email;
            db.Add(_user);
            db.SaveChangesAsync();

        }


        public string validateUsername(string username)
        {
            string query = (from i in db.Users
                where i.Username == username
                select i.UserId).FirstOrDefault();
            return query;
        }

        public string validateEmail(string email)
        {
            string query = (from i in db.Users
                where i.Email == email
                select i.UserId).FirstOrDefault();
            return query;
        }

        public void updateUsername(UsernameEntity user)
        {
           var query = (from i in db.Users
                where i.UserId == user.UserId
                      select i).FirstOrDefault();
           query.Username = user.Username;
           db.Update(query);
           db.SaveChangesAsync();

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

        public bool CheckUser(UsernameEntity user)
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