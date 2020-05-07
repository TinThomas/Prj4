using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OrctioneerSamlet.Models.Login
{
    public class PasswordEntity
    {
        [Key]
        public int id{get;set;}
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}