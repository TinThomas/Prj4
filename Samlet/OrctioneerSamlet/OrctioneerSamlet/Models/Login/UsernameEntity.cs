using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OrctioneerSamlet.Models.Login
{
    public class UsernameEntity
    {
        [Key]
        public int id{get;set;}

        [AllowNull]
        public string UserId { get; set; }
        [AllowNull]
        public string Username { get; set; }
        [AllowNull]
        public string Email { get; set; }
    }
}