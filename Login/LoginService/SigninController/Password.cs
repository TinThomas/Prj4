namespace SigninController
{
    public class Password
    {
       public bool validatePW(string a)
        {
            if (string.Compare(a, "world") == 0)
            {
                return true;
            }
            else return false;
        }

    }
}