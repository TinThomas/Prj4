using Login.Repos.WalletRepo;

namespace Login.WalletControl.Interfaces
{
    public interface IWallet
    {
        int getAmount(int id);
        void setWallet(int id, WalletModel wallet);
        void updateAmount(int id, int amount);
    }
}