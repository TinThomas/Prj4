using System.Linq;
using Login.SigninControl.Modules;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        public bool validatePassword(PasswordEntity pass)
        {
            var query = (from i in db.Passwords
                where i.UserId == pass.UserId
                select i).FirstOrDefault();
            if (query.Password == pass.Password)
            {
                return true;
            }

            return false;
        }

        public void CreatePassword(PasswordEntity password)
        {
            db.Passwords.Add(password);
            db.SaveChangesAsync();
        }

        public void updatePassword(PasswordEntity password)
        {
            var query = (from i in db.Passwords
                where i.UserId == password.UserId
                select i).FirstOrDefault();
            query.Password = password.Password;
            db.Passwords.Update(password);
            db.SaveChangesAsync();
        }

        public void DeletePassword(string id)
        {
            var query = (from i in db.Passwords
                where i.UserId == id
                select i).FirstOrDefault();
        }
    }
}