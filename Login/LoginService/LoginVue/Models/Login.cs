using System.ComponentModel.DataAnnotations;

namespace LoginVue.Models
{
    public class Login
    {
        public long loginRequest { get; set; }

        [Required]
        [MaxLength(254)]
        public string username { get; set; }

        [Required]
        [MaxLength(254)]
        public string email { get; set; }

        [Required]
        [MaxLength(254)]
        public string password { get; set; }

    }
}