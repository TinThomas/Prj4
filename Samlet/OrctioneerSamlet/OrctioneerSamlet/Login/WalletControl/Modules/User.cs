using Login.Repos.WalletRepo;
using Login.WalletControl.Interfaces;

namespace Login.WalletControl.Modules
{
    public class User : IUser
    {
        private WalletRepo myWallet;

        public User()
        {
            myWallet = WalletRepo.GetInstance();
        }

        public UserDataModel getUserData(int id)
        {
            UserDataModel myUser = new UserDataModel();
            myUser = myWallet.GetUser(id);
            return myUser;
        }

        public void setUserData(UserDataModel user)
        {
            myWallet.AddUser(user);
        }
    }
}