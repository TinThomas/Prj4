using Login.Repos.WalletRepo;
using Login.WalletControl.Interfaces;

namespace Login.WalletControl.Modules
{
    public class Card : ICard
    {
        private WalletRepo myWallet;

        public Card()
        {
            myWallet = WalletRepo.GetInstance();
        }

        public cardModel getCard(int id)
        {
            //not implemented
            return null;
        }

        public void setCard(int id, cardModel card)
        {
            myWallet.AddCard(id,card);
        }
    }
}