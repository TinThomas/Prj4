using Login.Repos.WalletRepo;
using Login.WalletControl.Interfaces;

namespace Login.WalletControl.Modules
{
    public class Wallet : IWallet
    {
        private WalletRepo myWallet;

        public Wallet()
        {
            myWallet = WalletRepo.GetInstance();
        }

        public int getAmount(int id)
        {
            return myWallet.GetAmount(id);
        }

        public void setWallet(int id, WalletModel wallet)
        {
            myWallet.AddWallet(id,wallet);
        }

        public void updateAmount(int id, int amount)
        {
            myWallet.AddAmount(id,amount);
        }
    }
}