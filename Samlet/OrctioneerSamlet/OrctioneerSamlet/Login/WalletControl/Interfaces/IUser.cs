namespace Login.WalletControl.Interfaces
{
    public interface IUser
    {
        UserDataModel getUserData(int id);
        void setUserData(UserDataModel user);
    }
}