namespace Login.WalletControl.Interfaces
{
    public interface ICard
    {
        cardModel getCard(int id);
        void setCard(int id, cardModel card);
    }
}