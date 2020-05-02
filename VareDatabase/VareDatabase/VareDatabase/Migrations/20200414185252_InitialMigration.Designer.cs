﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VareDatabase.DBContext;

namespace VareDatabase.Migrations
{
    [DbContext(typeof(VareDataModelContext))]
    [Migration("20200414185252_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VareDatabase.Models.BidEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemRef")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("userID_forLastBid")
                        .HasColumnType("int");

                    b.Property<int>("userID_forSeller")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ItemRef")
                        .IsUnique();

                    b.ToTable("BId");
                });

            modelBuilder.Entity("VareDatabase.Models.DescriptionEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemRef")
                        .HasColumnType("int");

                    b.Property<string>("descriptionOfItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageOfItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ItemRef")
                        .IsUnique();

                    b.ToTable("Description");
                });

            modelBuilder.Entity("VareDatabase.Models.ItemEntity", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("VareDatabase.Models.TimeEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemRef")
                        .HasColumnType("int");

                    b.Property<string>("experation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timeOfCreation")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ItemRef")
                        .IsUnique();

                    b.ToTable("Time");
                });

            modelBuilder.Entity("VareDatabase.Models.BidEntity", b =>
                {
                    b.HasOne("VareDatabase.Models.ItemEntity", "Item")
                        .WithOne("Bid")
                        .HasForeignKey("VareDatabase.Models.BidEntity", "ItemRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VareDatabase.Models.DescriptionEntity", b =>
                {
                    b.HasOne("VareDatabase.Models.ItemEntity", "Item")
                        .WithOne("Description")
                        .HasForeignKey("VareDatabase.Models.DescriptionEntity", "ItemRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VareDatabase.Models.TimeEntity", b =>
                {
                    b.HasOne("VareDatabase.Models.ItemEntity", "Item")
                        .WithOne("Time")
                        .HasForeignKey("VareDatabase.Models.TimeEntity", "ItemRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
