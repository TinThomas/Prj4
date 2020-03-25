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


        public void incomming(test a, string msg)
        {
            switch (a)
            {
                case test.login:
                {
                    if (string.Compare(msg,"") == 0)
                    {
                        Console.WriteLine(msg);
                        response();
                    }
                    break;
                    }

                case test.database:
                {
                    break;
                }
            }
        }

        public async Task<string> response()
        {
            return "Hello";
        }
    }
}