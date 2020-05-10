using System.Threading.Tasks;
using OrctioneerSamlet.Models.Login;
using VareDatabase;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IWalletRepository : IRepository<WalletEntity>
    {
        Task<int> getAmount(string id);
        Task<int> setAmount(string id, int amount);
        Task<WalletEntity> getDetails(string id);
        Task<int> addWallet(WalletEntity wallet);
        Task<int> updateWallet(WalletEntity wallet);
        Task<int> DeleteCard(string id);

    }
}