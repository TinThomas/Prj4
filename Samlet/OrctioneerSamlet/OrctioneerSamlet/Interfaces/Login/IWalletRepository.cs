using Login.WalletControl.Modules;
using OrctioneerSamlet.Models.Login;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IWalletRepository
    {
        void addUser(UserEntity user);
        void addWallet(WalletEntity wallet);
        void AddAddress(AddressEntity address);
        void updateUser(UserEntity user);
        void updateWallet(WalletEntity wallet);
        void updateAddress(AddressEntity address);
        void DeleteUser(string id);
        void DeleteAddress(string id);
        void DeleteCard(string id);

    }
}