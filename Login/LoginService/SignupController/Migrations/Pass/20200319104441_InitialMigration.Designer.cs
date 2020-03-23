﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SignupController;

namespace SignupController.Migrations.Pass
{
    [DbContext(typeof(PassContext))]
    [Migration("20200319104441_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("SignupController.Pass", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Password")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.ToTable("Passes");
                });
#pragma warning restore 612, 618
        }
    }
}
