using System.ComponentModel.DataAnnotations;

namespace LoginVue.Models
{
    public class Login
    {   
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

    public class internalMessage
    {
        public int id { get; set; }
        public string msg { get; set; }
    }
}