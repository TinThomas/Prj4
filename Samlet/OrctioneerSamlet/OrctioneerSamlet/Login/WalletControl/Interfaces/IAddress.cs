namespace Login.WalletControl.Interfaces
{
    public interface IAddress
    {
        AddressModel getAddress(int id);
        void setAddress(int id, AddressModel myAddress);
    }
}