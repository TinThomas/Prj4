﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace VareDatabase.Models
{
    public class TimeEntity
    {
        public string experation { get; set; }
        public DateTime timeOfCreation { get; set; }

        //Navigational property
        public ItemEntity Item { get; set; }
    }
}
