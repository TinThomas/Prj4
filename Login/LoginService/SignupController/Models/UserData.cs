using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SignupController.Models
{
    public class UserDataContext : DbContext
    {
        public DbSet<UserDataDB> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*This creates a one-to-one relationship between
             UserDataDB and AddressDB based on the AddressID property 
             in table UserDB (which is of type AddressDB) and the 
             AddressID property in table AddressDB (which is an int, and
             its primary key)
             This could be turned into a one-to-many*/
            modelBuilder.Entity<UserDataDB>()
                .HasOne(a => a.AddressID)
                .WithOne(b => b.UserData)
                .HasForeignKey<AddressDB>(b => b.AddressID);

            /*Creates one-to-one between WalletDB
             and UserDataDB*/
            modelBuilder.Entity<UserDataDB>()
                .HasOne(a => a.WalletID)
                .WithOne(b => b.UserID)
                .HasForeignKey<WalletDB>(b => b.CardID);

            /*Creates one-to-one between WalletDB
             and CardDB*/
            modelBuilder.Entity<WalletDB>()
                .HasOne(a => a.CardID)
                .WithOne(b => b.CardID)
                .HasForeignKey<CardDB>(b => b.CardID);
        }
    }
    public class UserDataDB
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }

        //The foreign key to the AddressDB
        public AddressDB AddressID { get; set; }
        //The foreign key to the WalletDB
        public WalletDB WalletID { get; set; }
    }

    public class AddressDB
    {
        [Key]
        public int AddressID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Address { get; set; }

        //The foreign key to the UserDataDB
        public UserDataDB UserData { get; set; }
    }

    public class WalletDB
    {
        [Key]
        public CardDB CardID { get; set; }
        public float Amount { get; set; }

        public UserDataDB UserID { get; set; }
    }

    public class CardDB
    {
        [Key]
        public int CardNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public int CVVNumber { get; set; }

        public WalletDB CardID { get; set; }
    }
}
