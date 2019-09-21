﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using properTech.Data;

namespace properTech.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("properTech.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("State");

                    b.Property<string>("StreetAddress");

                    b.Property<int>("ZipCode");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("properTech.Models.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<string>("BuildingName");

                    b.Property<int>("ManagerId");

                    b.Property<int>("PropertyId");

                    b.HasKey("BuildingId");

                    b.HasIndex("AddressId");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("properTech.Models.MaintenanceRequest", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActualCompletionDate");

                    b.Property<DateTime>("DateOfRequest");

                    b.Property<DateTime>("EstimatedCompletionDate");

                    b.Property<bool>("IsComplete");

                    b.Property<int>("MaintanenceTechId");

                    b.Property<string>("MaintenanceStatus");

                    b.Property<string>("Message");

                    b.Property<int>("ResidentId");

                    b.Property<int?>("ResidentId1");

                    b.Property<int>("confirmationNumber");

                    b.Property<string>("filePath");

                    b.Property<int?>("techMaintenanceTechId");

                    b.HasKey("RequestId");

                    b.HasIndex("ResidentId1");

                    b.HasIndex("techMaintenanceTechId");

                    b.ToTable("MaintenanceRequest");
                });

            modelBuilder.Entity("properTech.Models.MaintenanceTech", b =>
                {
                    b.Property<int>("MaintenanceTechId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("AverageDeviation");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("MaintenanceTechId");

                    b.ToTable("MaintenanceTech");
                });

            modelBuilder.Entity("properTech.Models.Manager", b =>
                {
                    b.Property<int>("ManagerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("ManagerId");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("properTech.Models.MonthlyRevenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Month");

                    b.Property<double>("Revenue");

                    b.HasKey("Id");

                    b.ToTable("MonthlyRevenue");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Month = "January",
                            Revenue = 5000.0
                        },
                        new
                        {
                            Id = 2,
                            Month = "February",
                            Revenue = 5400.0
                        },
                        new
                        {
                            Id = 3,
                            Month = "March",
                            Revenue = 4800.0
                        },
                        new
                        {
                            Id = 4,
                            Month = "April",
                            Revenue = 6000.0
                        },
                        new
                        {
                            Id = 5,
                            Month = "May",
                            Revenue = 5000.0
                        },
                        new
                        {
                            Id = 6,
                            Month = "June",
                            Revenue = 4200.0
                        },
                        new
                        {
                            Id = 7,
                            Month = "July",
                            Revenue = 5600.0
                        },
                        new
                        {
                            Id = 8,
                            Month = "August",
                            Revenue = 5800.0
                        });
                });

            modelBuilder.Entity("properTech.Models.OccupancyPercent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Month");

                    b.Property<double>("OccupancyPercentage");

                    b.HasKey("Id");

                    b.ToTable("OccupancyPercent");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Month = "January",
                            OccupancyPercentage = 0.75
                        },
                        new
                        {
                            Id = 2,
                            Month = "February",
                            OccupancyPercentage = 0.80000000000000004
                        },
                        new
                        {
                            Id = 3,
                            Month = "March",
                            OccupancyPercentage = 0.78000000000000003
                        },
                        new
                        {
                            Id = 4,
                            Month = "April",
                            OccupancyPercentage = 0.75
                        },
                        new
                        {
                            Id = 5,
                            Month = "May",
                            OccupancyPercentage = 0.80000000000000004
                        },
                        new
                        {
                            Id = 6,
                            Month = "June",
                            OccupancyPercentage = 0.68999999999999995
                        },
                        new
                        {
                            Id = 7,
                            Month = "July",
                            OccupancyPercentage = 0.75
                        },
                        new
                        {
                            Id = 8,
                            Month = "August",
                            OccupancyPercentage = 0.88
                        });
                });

            modelBuilder.Entity("properTech.Models.Property", b =>
                {
                    b.Property<int>("PropertyId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<int>("ManagerId");

                    b.Property<string>("PropertyName");

                    b.HasKey("PropertyId");

                    b.HasIndex("AddressId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("properTech.Models.QuarterlyEarnings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Earnings");

                    b.Property<string>("Quarter");

                    b.HasKey("Id");

                    b.ToTable("QuarterlyEarnings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Earnings = 3000.0,
                            Quarter = "1"
                        },
                        new
                        {
                            Id = 2,
                            Earnings = 3500.0,
                            Quarter = "2"
                        });
                });

            modelBuilder.Entity("properTech.Models.Resident", b =>
                {
                    b.Property<int>("ResidentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<double>("Balance");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LatePayment");

                    b.Property<DateTime>("LeaseEnd");

                    b.Property<DateTime>("LeaseStart");

                    b.Property<DateTime>("PaymentDueDate");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("RenewedLease");

                    b.Property<int>("UnitId");

                    b.Property<int>("UnitNumber");

                    b.Property<bool>("isAssignedUnit");

                    b.Property<int>("maintenanceRequestId");

                    b.HasKey("ResidentId");

                    b.ToTable("Resident");
                });

            modelBuilder.Entity("properTech.Models.UnassignedUsers", b =>
                {
                    b.Property<int>("UnassignedId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("Email");

                    b.HasKey("UnassignedId");

                    b.ToTable("UnassignedUsers");
                });

            modelBuilder.Entity("properTech.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId");

                    b.Property<int>("BathroomCount");

                    b.Property<int>("BuildingId");

                    b.Property<bool>("IsOccupied");

                    b.Property<int>("ManagerId");

                    b.Property<double>("MonthlyRent");

                    b.Property<int>("RoomCount");

                    b.Property<int>("SquareFootage");

                    b.Property<int>("UnitNumber");

                    b.HasKey("UnitId");

                    b.HasIndex("AddressId");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("properTech.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Role");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("properTech.Models.Building", b =>
                {
                    b.HasOne("properTech.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("properTech.Models.MaintenanceRequest", b =>
                {
                    b.HasOne("properTech.Models.Resident", "resident")
                        .WithMany()
                        .HasForeignKey("ResidentId1");

                    b.HasOne("properTech.Models.MaintenanceTech", "tech")
                        .WithMany()
                        .HasForeignKey("techMaintenanceTechId");
                });

            modelBuilder.Entity("properTech.Models.Property", b =>
                {
                    b.HasOne("properTech.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("properTech.Models.Unit", b =>
                {
                    b.HasOne("properTech.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
