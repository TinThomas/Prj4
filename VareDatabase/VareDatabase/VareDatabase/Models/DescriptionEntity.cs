using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VareDatabase.Models
{

    public class DescriptionEntity
    {
        [Key]
        public int ID { get; set; }
        public string imageOfItem { get; set; }
        public string descriptionOfItem { get; set; }
        public string title { get; set; }

        //Navigational property
        public int ItemRef { get; set; }
        public ItemEntity Item { get; set; }
    }
}
