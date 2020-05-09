using System.Threading.Tasks;
using OrctioneerSamlet.Models.Login;
using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IAddressRepository : IRepository<AddressEntity>
    {
        Task<AddressEntity> GetAddress(string id);
        Task<int> addAddress(AddressEntity address, string id);

        Task<int> updateAddress(AddressEntity address, string id);
        Task<int> DeleteAddress(string id);
    }
}