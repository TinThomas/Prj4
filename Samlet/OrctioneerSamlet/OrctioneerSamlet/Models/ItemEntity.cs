﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VareDatabase.Models
{

    public class ItemEntity
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [MaxLength(120)]
        public string Title { get; set; }
        [Required]
        public int BuyOutPrice { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required] public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required] public int UserIdSeller { get; set; }
        [Required]
        [MaxLength(500)]
        public string DescriptionOfItem { get; set; }
        public bool Sold { get; set; } = false;

        //Navigational property
        public ICollection<ImageEntity> Images { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
        public ICollection<BidEntity> Bids { get; set; }
        public ItemEntity()
        {
            DateCreated = DateTime.Now;
            Images = new List<ImageEntity>();
            Tags = new List<TagEntity>();
            Bids = new List<BidEntity>();
        }
    }
}