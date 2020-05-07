namespace Login.SigninControl.Interfaces
{
    public interface ISigninControl
    {
        int validateUsername(string msg);
        bool validatePW(int id,string msg);
    }
}