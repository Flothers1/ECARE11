﻿// <auto-generated />
using System;
using ECARE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ECARE.Migrations
{
    [DbContext(typeof(ECAREContext))]
    [Migration("20250407142654_MyMigrationvvdfafv")]
    partial class MyMigrationvvdfafv
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ECARE.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("LabBranchId")
                        .HasColumnType("int");

                    b.Property<int?>("LabId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("LabBranchId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ECARE.Models.Lab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LabName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lab");
                });

            modelBuilder.Entity("ECARE.Models.LabBranch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LabBranchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LabId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.ToTable("LabBranch");
                });

            modelBuilder.Entity("ECARE.Models.PatientRegistrations", b =>
                {
                    b.Property<int>("PatientRegistrationsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientRegistrationsId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("AgeGroup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Age Group]");

                    b.Property<string>("CaregiverName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Caregiver Name]");

                    b.Property<string>("CaregiverPhone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Caregiver Phone]");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CopyOfIDOrPassportPathBack")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CopyOfIDOrPassportFileBack");

                    b.Property<string>("CopyOfIDOrPassportPathFront")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CopyOfIDOrPassportFileFront");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Government")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HCPGovernment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[HCP Government ]");

                    b.Property<string>("HCPTerritory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[HCP Territory ]");

                    b.Property<string>("HealthcareProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Healthcare Provider]");

                    b.Property<string>("HealthcareProviderAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Healthcare Provider Address]");

                    b.Property<string>("Indication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("Medication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Membership Number]");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[National ID]");

                    b.Property<string>("OTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OTPExpiration")
                        .HasColumnType("datetime2");

                    b.Property<string>("PatientAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Patient Name]");

                    b.Property<string>("PhoneNumber1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Phone Number1]");

                    b.Property<string>("PhoneNumber2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Phone Number2]");

                    b.Property<string>("Prescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramSponsor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("[Program Sponsor]");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("[Registration Date]");

                    b.Property<DateTime>("StartDateOfMedication")
                        .HasColumnType("datetime2")
                        .HasColumnName("[Start Date Of Medication]");

                    b.Property<string>("Territory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestDocument1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestDocument2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientRegistrationsId");

                    b.ToTable("PatientRegistrations");
                });

            modelBuilder.Entity("ECARE.Models.Service_Request", b =>
                {
                    b.Property<int>("Service_RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Service_RequestId"));

                    b.Property<double?>("CoPaymentPercentage")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("EVoucherPDF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LabBranchId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientRegistrationsId")
                        .HasColumnType("int");

                    b.Property<bool>("Payment")
                        .HasColumnType("bit");

                    b.Property<string>("RequiredTests")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Service_RequestId");

                    b.HasIndex("LabBranchId");

                    b.HasIndex("PatientRegistrationsId");

                    b.ToTable("ServiceRequests");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ECARE.Models.ApplicationUser", b =>
                {
                    b.HasOne("ECARE.Models.LabBranch", "LabBranch")
                        .WithMany("ApplicationUser")
                        .HasForeignKey("LabBranchId");

                    b.Navigation("LabBranch");
                });

            modelBuilder.Entity("ECARE.Models.LabBranch", b =>
                {
                    b.HasOne("ECARE.Models.Lab", "Lab")
                        .WithMany("LabBranch")
                        .HasForeignKey("LabId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lab");
                });

            modelBuilder.Entity("ECARE.Models.Service_Request", b =>
                {
                    b.HasOne("ECARE.Models.LabBranch", "LabBranch")
                        .WithMany()
                        .HasForeignKey("LabBranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECARE.Models.PatientRegistrations", "PatientRegistrations")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("PatientRegistrationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LabBranch");

                    b.Navigation("PatientRegistrations");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ECARE.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ECARE.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECARE.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ECARE.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECARE.Models.Lab", b =>
                {
                    b.Navigation("LabBranch");
                });

            modelBuilder.Entity("ECARE.Models.LabBranch", b =>
                {
                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("ECARE.Models.PatientRegistrations", b =>
                {
                    b.Navigation("ServiceRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
