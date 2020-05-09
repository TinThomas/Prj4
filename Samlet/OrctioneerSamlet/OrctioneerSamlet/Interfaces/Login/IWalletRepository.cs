using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IWalletRepository : IRepository<WalletEntity>
    {
        int getAmount(string id);
        WalletEntity getDetails(string id);
        void addWallet(WalletEntity wallet);
        void updateWallet(WalletEntity wallet);
        void DeleteCard(string id);

    }
}