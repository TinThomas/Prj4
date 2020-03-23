using System.ComponentModel.DataAnnotations;

namespace SigninController
{
    public class UserName
    {
        
        public int getUserId(string input)
        {
            int id;
            //some fancy db calls.
            if (isUsername(input))
            { 
                id = 42;
            }
            else if (isEmail(input))
            {
                id = 24;
            }
            else
            {
                id = -1;
            }

            return id;
        }

        public bool isUsername(string input)
        {
            //fancy compare
            if (true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool isEmail(string input)
        {
            if (true)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}