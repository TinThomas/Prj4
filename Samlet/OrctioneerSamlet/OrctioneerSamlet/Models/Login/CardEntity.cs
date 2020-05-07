using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace OrctioneerSamlet.Models.Login
{
    public class CardEntity
    {
        [Key]
        public int id { get; set; }
        [AllowNull]
        public int CardId { get; set; }
        [AllowNull]
        public long CardNumber { get; set; }
        [AllowNull]
        public int ExpireMonth { get; set; }
        [AllowNull]
        public int ExpireYear { get; set; }
        [AllowNull]
        public int CVVnumber { get; set; }
        
    }
}