namespace Login.WalletControl.Interfaces
{
    public interface IWalletControl
    {
        int getAmount(int id);
        void setAmount(int id, int value);
        UserDataModel getUser(int id);
    }
}