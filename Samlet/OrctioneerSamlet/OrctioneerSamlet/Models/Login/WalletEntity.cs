using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OrctioneerSamlet.Models.Login
{
    public class WalletEntity
    {
        [Key]
        public int id { get; set; }

        public string userID { get; set; } = "";
        [AllowNull]
        public CardEntity card { get; set; }

        [AllowNull] public int Amount { get; set; } = 0;
    }
}