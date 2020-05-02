using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace VareDatabase.Models
{
    public class TimeEntity
    {
        [Key]
        public int ID { get; set; }
        public string experation { get; set; }
        public DateTime timeOfCreation { get; set; }

        //Navigational property
        public int ItemRef { get; set; }
        public ItemEntity Item { get; set; }
    }
}
