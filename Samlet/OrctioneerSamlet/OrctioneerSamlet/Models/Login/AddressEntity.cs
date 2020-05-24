using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OrctioneerSamlet.Models.Login
{
    public class AddressEntity
    {
        [Key]
        public int id { get; set; }
        [AllowNull]
        public int AddressId { get; set; }

        [AllowNull] public string Contry { get; set; } = "";
        [AllowNull] public string City { get; set; } = "";
        [AllowNull] public int Zipcode { get; set; } = 0;
        [AllowNull] public string Address { get; set; } = "";
    }
}