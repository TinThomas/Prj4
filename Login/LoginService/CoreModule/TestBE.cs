using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;
using SigninController;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CoreModule
{
    public class TestBE
    {
        private Signin myLogin;
        public enum test
        {
            login,
            database
        };

        public TestBE()
        {
            myLogin = new Signin();
        }
        public async Task<bool> incomming(int a, int b, string msg)
        {
            switch (a)
            {
                case 1:
                {
                    switch (b)
                    {
                        case 1:
                        {
                            return myLogin.validateUsername(msg);
                            break;
                        }
                        case 2:
                        {
                            return myLogin.validatePassword(msg);
                            break;
                        }
                        default:
                        {
                            return false;
                            break;
                        }
                    }
                    break;
                    }

                case 2:
                {
                    return false;
                    break;
                }
                default:
                {
                    return false;
                    break;
                }
                
            }

            return false;
        }

    }

    public class userInfo
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}