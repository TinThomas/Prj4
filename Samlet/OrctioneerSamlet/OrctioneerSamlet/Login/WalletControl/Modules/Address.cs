using Login.Repos.WalletRepo;
using Login.WalletControl.Interfaces;

namespace Login.WalletControl.Modules
{
    public class Address : IAddress
    {
        private WalletRepo myWallet;

        public Address()
        {
            myWallet = WalletRepo.GetInstance();
        }

        public AddressModel getAddress(int id)
        {
            AddressModel myAddress = new AddressModel();
            myAddress = myWallet.GetAddress(id);
            return myAddress;
        }

        public void setAddress(int id, AddressModel myAddress)
        {
            myWallet.AddAddress(id,myAddress);
        }
    }
}