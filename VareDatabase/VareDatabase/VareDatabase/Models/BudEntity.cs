﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace VareDatabase.Models
{

    public class BudEntity
    {
        public int userID_forLastBid { get; set; } 
        public int price { get; set; }
        public int ÚuerID_forSeller { get; set; }

        //Navigational property
        public ItemEntity Item { get; set; }
    }
}

