using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace OrctioneerSamlet.Models.Login
{
    public class UserEntity
    {
        [Key]
        public int id { get; set; }

        public string userID { get; set; }
        [AllowNull]
        public string FirstName { get; set; }
        [AllowNull]
        public string LastName { get; set; }
        [AllowNull]
        public AddressEntity Address { get; set; }
        [AllowNull]
        public WalletEntity wallet { get; set; }
        
    }
}