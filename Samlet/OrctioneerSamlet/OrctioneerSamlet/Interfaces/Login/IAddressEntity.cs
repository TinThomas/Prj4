using VareDatabase;
using VareDatabase.Repo;

namespace OrctioneerSamlet.Interfaces.Login
{
    public interface IAddressEntity : IRepository<AddressRepository>
    {
        AddressRepository GetAddressEntity(string id);
        void addAddress(AddressRepository address);

        void updateAddress(AddressRepository address);
        void DeleteAddress(string id);
    }
}