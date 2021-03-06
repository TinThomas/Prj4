﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VareDatabase.DBContext;

namespace OrctioneerSamlet.Migrations.Login
{
    [DbContext(typeof(WalletContext))]
    partial class WalletContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.AddressEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Zipcode")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.CardEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVVnumber")
                        .HasColumnType("int");

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("ExpireMonth")
                        .HasColumnType("int");

                    b.Property<int>("ExpireYear")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.UserEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Addressid")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("walletid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Addressid");

                    b.HasIndex("walletid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.WalletEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("cardid")
                        .HasColumnType("int");

                    b.Property<string>("userID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("cardid");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.UserEntity", b =>
                {
                    b.HasOne("OrctioneerSamlet.Models.Login.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("Addressid");

                    b.HasOne("OrctioneerSamlet.Models.Login.WalletEntity", "wallet")
                        .WithMany()
                        .HasForeignKey("walletid");
                });

            modelBuilder.Entity("OrctioneerSamlet.Models.Login.WalletEntity", b =>
                {
                    b.HasOne("OrctioneerSamlet.Models.Login.CardEntity", "card")
                        .WithMany()
                        .HasForeignKey("cardid");
                });
#pragma warning restore 612, 618
        }
    }
}
