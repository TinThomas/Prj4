using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Models
{

    public class BidEntity
    {
        [Key]
        public int ID { get; set; }
        public int userID_forLastBid { get; set; } 
        public int price { get; set; }
        public int userID_forSeller { get; set; }

        //Navigational property
        public int ItemRef { get; set; }
        public ItemEntity Item { get; set; }
    }
}

