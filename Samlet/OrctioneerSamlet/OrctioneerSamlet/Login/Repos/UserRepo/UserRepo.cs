using System;
using System.Collections.Generic;

namespace Login.Repos.UserRepo
{
    public class UserRepo
    {
        private static UserRepo instance = null;
        private readonly Dictionary<int, UserModel> users;

        private UserRepo()
        {
            users = new Dictionary<int, UserModel>();
            new List<UserModel>
            {
                new UserModel
                {
                    userID = 1,
                    userName = "Admin",
                    Email = "Admin@Orctioneer.com"
                },
                new UserModel
                {
                    userID = 2,
                    userName = "TestUser1",
                    Email = "Test@Orctioneer.com"
                },
                new UserModel
                {
                    userID = 20,
                    userName = "Jonas",
                    Email = "Jonas@hejsa.com"
                }
            }.ForEach(r => AddUser(r));
        }

        static public UserRepo GetInstance()
        {
            if (instance == null)
            {
                instance = new UserRepo();
            }

            return instance;
        }

        public int GetUserIdByName( string username)
        {
            int index = 0;
            foreach (var i in users)
            {
                if (string.Compare(username,i.Value.userName) == 0)
                {
                    return index = i.Value.userID;
                }
            }

            return -1;
        }

        public int GetUserIdByEmail(string email)
        {
            int index = 0;
            foreach (var i in users)
            {
                if (string.Compare(email,i.Value.Email) == 0)
                {
                    return index = i.Value.userID;
                }
            }

            return -1;
        }

        public bool AddUser(UserModel user)
        {
            if (user.userID == 0)
            {
                int key = users.Count;
                while (users.ContainsKey(key))
                {
                    key++;
                }

                user.userID = key;
            }
            users[user.userID] = user;
            return true;
        }
    }
}