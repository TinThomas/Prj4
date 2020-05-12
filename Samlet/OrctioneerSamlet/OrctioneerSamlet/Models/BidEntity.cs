using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace VareDatabase.Models
{

    public class BidEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Bid { get; set; }
        [Required]
        public string UserIdBuyer { get; set; }

        //Navigational property
        public ItemEntity Item { get; set; }
        public int ItemId { get; set; }
        public DateTime Created { get; set; }

        public BidEntity()
        {
            Created = DateTime.Now;
        }
    }
}