﻿// <auto-generated />
using System;
using DAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SelfDrivingCarRentalPlatform.Migrations
{
    [DbContext(typeof(SdcrpDbContext))]
    partial class SdcrpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObjects.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarBrandId")
                        .HasColumnType("int");

                    b.Property<string>("CarModel")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CarOwnerId")
                        .HasColumnType("int");

                    b.Property<string>("CarRegisterNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("CarTypeId")
                        .HasColumnType("int");

                    b.Property<int>("DepositRatio")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsElectric")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMortgageRequired")
                        .HasColumnType("bit");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<double>("PricePerDay")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarBrandId");

                    b.HasIndex("CarOwnerId");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("PlateNumber")
                        .IsUnique();

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("BusinessObjects.Models.CarBrand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BrandDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CarBrands");
                });

            modelBuilder.Entity("BusinessObjects.Models.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TypeDescription")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CarTypes");
                });

            modelBuilder.Entity("BusinessObjects.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RentEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RentStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SignDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("CarId", "RentStartDate")
                        .IsUnique();

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("BusinessObjects.Models.DrivingLicense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DrivingLicenseNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DrivingLicenses");
                });

            modelBuilder.Entity("BusinessObjects.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("BusinessObjects.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<double?>("DamageFee")
                        .HasColumnType("float");

                    b.Property<double>("Deposit")
                        .HasColumnType("float");

                    b.Property<double>("InsuranceFee")
                        .HasColumnType("float");

                    b.Property<double?>("LateReturnPenalty")
                        .HasColumnType("float");

                    b.Property<double>("MortgageFee")
                        .HasColumnType("float");

                    b.Property<double?>("OtherFees")
                        .HasColumnType("float");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("DrivingLicenseId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrivingLicenseId")
                        .IsUnique()
                        .HasFilter("[DrivingLicenseId] IS NOT NULL");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("LocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessObjects.Models.Car", b =>
                {
                    b.HasOne("BusinessObjects.Models.CarBrand", "CarBrand")
                        .WithMany("Cars")
                        .HasForeignKey("CarBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Models.User", "CarOwner")
                        .WithMany("Cars")
                        .HasForeignKey("CarOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Models.CarType", "CarType")
                        .WithMany("Cars")
                        .HasForeignKey("CarTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarBrand");

                    b.Navigation("CarOwner");

                    b.Navigation("CarType");
                });

            modelBuilder.Entity("BusinessObjects.Models.Contract", b =>
                {
                    b.HasOne("BusinessObjects.Models.Car", "Car")
                        .WithMany("Contracts")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Models.User", "Customer")
                        .WithMany("Contracts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BusinessObjects.Models.Transaction", b =>
                {
                    b.HasOne("BusinessObjects.Models.Contract", "Contract")
                        .WithOne("Transaction")
                        .HasForeignKey("BusinessObjects.Models.Transaction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.HasOne("BusinessObjects.Models.DrivingLicense", "DrivingLicense")
                        .WithOne("Owner")
                        .HasForeignKey("BusinessObjects.Models.User", "DrivingLicenseId");

                    b.HasOne("BusinessObjects.Models.Location", "Location")
                        .WithMany("Users")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrivingLicense");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("BusinessObjects.Models.Car", b =>
                {
                    b.Navigation("Contracts");
                });

            modelBuilder.Entity("BusinessObjects.Models.CarBrand", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("BusinessObjects.Models.CarType", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("BusinessObjects.Models.Contract", b =>
                {
                    b.Navigation("Transaction")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Models.DrivingLicense", b =>
                {
                    b.Navigation("Owner")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObjects.Models.Location", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Contracts");
                });
#pragma warning restore 612, 618
        }
    }
}
