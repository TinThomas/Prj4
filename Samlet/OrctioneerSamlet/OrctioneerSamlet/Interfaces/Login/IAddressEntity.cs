using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IAddressEntity : IRepository<AddressEntity>
    {
        AddressEntity GetAddressEntity(string id);
        void addAddress(AddressEntity address);

        void updateAddress(AddressEntity address);
        void DeleteAddress(string id);
    }
}