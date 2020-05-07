using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using OrctioneerSamlet.Interfaces.Login;
using OrctioneerSamlet.Models.Login;
using VareDatabase.DBContext;

namespace VareDatabase.Repo
{
    public class UsernameRepository : IUsernameRepository
    {
        private UserModelContext db;

        public UsernameRepository(UserModelContext _db)
        {
            db = _db;
        }

        public void addUser(string username,string email)
        {
            string id = Guid.NewGuid().ToString();
            UsernameEntity user = new UsernameEntity();
            user.UserId = id;
            user.Username = username;
            user.Email = email;
            db.Add(user);
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

        public void updateUsername(string id, string username)
        {
           var query = (from i in db.Users
                where i.UserId == id
                      select i).FirstOrDefault();
           query.Username = username;
           db.Update(query);
           db.SaveChangesAsync();

        }

        public void updateEmail(string id, string email)
        {
            var query = (from i in db.Users
                where i.UserId == id
                select i).FirstOrDefault();
            query.Email = email;
            db.Update(query);
            db.SaveChangesAsync();
        }

        public bool CheckUser(string username, string email)
        {
            var query = (from i in db.Users
                where i.Username == username
                select i).FirstOrDefault();
            if (query != null)
            {
                return false;
            }

            query = (from i in db.Users
                where i.Email == email
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