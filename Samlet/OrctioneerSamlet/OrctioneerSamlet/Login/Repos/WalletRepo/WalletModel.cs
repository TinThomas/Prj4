namespace Login.Repos.WalletRepo
{
    public class WalletModel
    {
        public int userID { get; set; } = 0;
        public int cardID { get; set; } = 0;
        public int Amount { get; set; } = 0;
    }
}

public class cardModel
{
    public long Cardnumber { get; set; } = 0;
    public int cardID { get; set; } = 0;
    public int expireMonth { get; set; } = 0;
    public int expireYear { get; set; } = 0;
    public int CVVnumber { get; set; } = 0;
}

public class AddressModel
{
    public int AddressID { get; set; } = 0;
    public string Contry { get; set; } = "";
    public string City { get; set; } = "";
    public int zipcode { get; set; } = 0;
    public string Address { get; set; } = "";
}

public class UserDataModel
{
    public int userID { get; set; } = 0;
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public int AddressID { get; set; } = 0;
}