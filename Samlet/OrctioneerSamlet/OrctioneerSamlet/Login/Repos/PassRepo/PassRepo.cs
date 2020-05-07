using System.Collections.Generic;

namespace Login.Repos.PassRepo
{
    public class PassRepo
    {
        private static PassRepo instance = null;
        private readonly Dictionary<long, PassModel> passwords;

        private PassRepo()
        {
            passwords = new Dictionary<long, PassModel>();
            new List<PassModel>
            {
                new PassModel
                {
                    userId = 1,
                    Password = "Password1"
                },
                new PassModel
                {
                    userId = 2,
                    Password = "TestPass1"
                },
                new PassModel
                {
                    userId = 20,
                    Password = "ASimplePass12"
                }

            }.ForEach(r => AddPassword(r));
        }

        static public PassRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new PassRepo();
            }

            return instance;
        }

        public bool CheckPassword(int id, string password)
        {
            var myPass = passwords[id];
            if (myPass.Password == password)
            {
                return true;
            }

            return false;
        }

        public bool AddPassword(PassModel pass)
        {
            if (passwords.ContainsKey(pass.userId))
            {
                return false;
            }
            passwords[pass.userId] = pass;
            return true;
        }

    }
}