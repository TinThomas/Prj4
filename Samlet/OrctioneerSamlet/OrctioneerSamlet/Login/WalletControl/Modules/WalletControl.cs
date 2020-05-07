using Login.WalletControl.Interfaces;
using Login.WalletControl.Modules;

namespace Login.WalletControl
{
    public class WalletControl : IWalletControl
    {
        private IAddress myAddress;
        private IUser myUser;
        private IWallet myWallet;
        private ICard myCard;

        public WalletControl(IAddress address, IUser user, IWallet wallet, ICard card)
        {
            myAddress = address;
            myUser = user;
            myWallet = wallet;
            myCard = card;
        }

        public int getAmount(int id)
        {
            return myWallet.getAmount(id);
        }

        public void setAmount(int id,int value)
        {
            myWallet.updateAmount(id,value);
        }

        public UserDataModel getUser(int id)
        {
            UserDataModel myModel = new UserDataModel();
            myModel = myUser.getUserData(id);
            return myModel;
        }
    }
}