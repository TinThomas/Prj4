using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualBasic;

namespace CoreModule
{
    public class TestBE
    {
        public enum test
        {
            login,
            database
        };

        public async Task<string> incomming(int a, string msg)
        {
            switch (a)
            {
                case 1:
                {

                        Console.WriteLine(msg);
                        return response();
                        break;
                    }

                case 2:
                {
                    return "you";
                    break;
                }
                default:
                {
                    return "b";
                    break;
                }
                
            }

            return "";
        }

        public string response()
        {
            return "Hello";
        }

    }
}