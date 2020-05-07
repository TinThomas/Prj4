using System.Collections.Generic;

namespace Login.Repos.WalletRepo
{
    public class WalletRepo
    {
        private static WalletRepo instance = null;
        private readonly Dictionary<int, WalletModel> wallets;
        private readonly Dictionary<int, UserDataModel> userdata;
        private readonly Dictionary<int, AddressModel> address;
        private readonly Dictionary<int, cardModel> cards;

        private WalletRepo()
        {
            wallets = new Dictionary<int, WalletModel>();
            userdata = new Dictionary<int, UserDataModel>();
            address = new Dictionary<int, AddressModel>();
            cards = new Dictionary<int, cardModel>();
            new List<UserDataModel>
            {
                new UserDataModel
                {
                    userID = 1,
                    FirstName = "Hans",
                    LastName = "Peter",
                    AddressID = 1,
                },
                new UserDataModel
                {
                    userID = 2,
                    FirstName = "Sven",
                    LastName = "Erik",
                    AddressID = 2
                },
                new UserDataModel
                {
                    userID = 20,
                    FirstName = "Jonas",
                    LastName = "Nielsen",
                    AddressID = 3
                }
            }.ForEach(r => AddUser(r));

            int[] ids = {1, 2, 20};
            new List<AddressModel>
            {
                new AddressModel
                {
                    AddressID = 1,
                    Address = "Kongegade 20",
                    City = "Horsen",
                    Contry = "Danmark",
                    zipcode = 8700
                },
                new AddressModel
                {
                    AddressID = 2,
                    Address = "FantasyStreet 420",
                    City = "København K",
                    Contry = "Danmark",
                    zipcode = 1110
                },
                new AddressModel
                {
                    AddressID = 3,
                    Address = "Spobjergvej 153",
                    City = "Brabrand",
                    Contry = "Danmark",
                    zipcode = 8220
                }
            }.ForEach(r => AddAddress(r));

            new List<WalletModel>
            {
                new WalletModel
                {
                    userID = 1,
                    cardID = 4,
                    Amount = 5000
                },
                new WalletModel
                {
                    userID = 2,
                    cardID = 25,
                    Amount = 400000
                },
                new WalletModel
                {
                    userID = 20,
                    cardID = 520,
                    Amount = 10
                }
            }.ForEach(r => AddWallet(r));

            new List<cardModel>
            {
                new cardModel
                {
                    cardID = 4,
                    Cardnumber = 7171924069803669,
                    expireMonth = 02,
                    expireYear = 25,
                    CVVnumber = 482
                },
                new cardModel
                {
                    cardID = 25,
                    Cardnumber = 5437442198354380,
                    expireYear = 23,
                    expireMonth = 04,
                    CVVnumber = 293
                },
                new cardModel
                {
                    cardID = 520,
                    Cardnumber = 6310144817556693,
                    expireYear = 26,
                    expireMonth = 12,
                    CVVnumber = 267
                }
            }.ForEach(r => AddCard(r));
        }

        static public WalletRepo GetInstance()
        {
            if(instance == null)
                instance = new WalletRepo();
            return instance;
        }

        public UserDataModel GetUser(int id)
        {
            var user = userdata[id];
            return user;
        }

        public int GetAmount(int id)
        {
            var userId = userdata[id];
            return wallets[userId.userID].Amount;
        }

        public AddressModel GetAddress(int id)
        {
            var userId = userdata[id];
            return address[userId.AddressID];
        }

        public void AddUser(UserDataModel user)
        {
            if (user.userID == 0)
            {
                int key = userdata.Count;
                while (userdata.ContainsKey(key))
                {
                    key++;
                }

                user.userID = key;
            }

            userdata[user.userID] = user;
        }

        private void AddWallet(WalletModel myWallet)
        {
            wallets[myWallet.userID] = myWallet;
        }


        public void AddWallet(int id,WalletModel mywallet)
        {
            var userid = userdata[id];
            WalletModel _Wallet = new WalletModel();
            _Wallet = mywallet;
            _Wallet.userID = id;
            wallets[userid.userID] = _Wallet;
        }

        public void AddAmount(int id, int amount)
        {
            var userid = userdata[id];
            wallets[userid.userID].Amount += amount;
        }

        private void AddAddress(AddressModel _address)
        { 
            address[_address.AddressID] = _address;
        }

        public void AddAddress(int id, AddressModel address)
        {
            var userid = userdata[id];
            AddressModel _address = new AddressModel();
            _address = address;
            _address.AddressID = userid.AddressID;
            this.address[userid.AddressID] = _address;
        }

        private void AddCard(cardModel card)
        {
            cards[card.cardID] = card;
        }

        public void AddCard(int id, cardModel card)
        {
            var userid = userdata[id];
            var walletid = wallets[userid.userID];
            cardModel _card = new cardModel();
            _card = card;
            _card.cardID = walletid.cardID;
            cards[walletid.cardID] = _card;
        }
    }
}