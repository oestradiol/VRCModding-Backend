﻿// <auto-generated />
using System;
using VRCModding.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace VRCModding.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("VRCModding.Entities.AccountInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("DisplayNameFK")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserFK")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DisplayNameFK")
                        .IsUnique();

                    b.HasIndex("UserFK");

                    b.ToTable("AccountInfos");
                });

            modelBuilder.Entity("VRCModding.Entities.DisplayName", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Name");

                    b.ToTable("DisplayNames");
                });

            modelBuilder.Entity("VRCModding.Entities.Hwid", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserFK")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserFK");

                    b.ToTable("Hwids");
                });

            modelBuilder.Entity("VRCModding.Entities.Ip", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserFK")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserFK");

                    b.ToTable("Ips");
                });

            modelBuilder.Entity("VRCModding.Entities.UsedDisplayName", b =>
                {
                    b.Property<string>("AccountInfoFK")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("DisplayNameFK")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("FirstSeen")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUsage")
                        .HasColumnType("datetime(6)");

                    b.HasKey("AccountInfoFK", "DisplayNameFK");

                    b.HasIndex("DisplayNameFK");

                    b.ToTable("UsedDisplayNames");
                });

            modelBuilder.Entity("VRCModding.Entities.User", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationDateUtc")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Name");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VRCModding.Entities.AccountInfo", b =>
                {
                    b.HasOne("VRCModding.Entities.DisplayName", "CurrentDisplayName")
                        .WithOne("CurrentAccount")
                        .HasForeignKey("VRCModding.Entities.AccountInfo", "DisplayNameFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VRCModding.Entities.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserFK");

                    b.Navigation("CurrentDisplayName");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VRCModding.Entities.Hwid", b =>
                {
                    b.HasOne("VRCModding.Entities.User", "User")
                        .WithMany("KnownHWIDs")
                        .HasForeignKey("UserFK");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VRCModding.Entities.Ip", b =>
                {
                    b.HasOne("VRCModding.Entities.User", "User")
                        .WithMany("KnownIPs")
                        .HasForeignKey("UserFK");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VRCModding.Entities.UsedDisplayName", b =>
                {
                    b.HasOne("VRCModding.Entities.AccountInfo", "AccountInfo")
                        .WithMany("DisplayNameHistory")
                        .HasForeignKey("AccountInfoFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VRCModding.Entities.DisplayName", "DisplayName")
                        .WithMany("HasBeenUsedBy")
                        .HasForeignKey("DisplayNameFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountInfo");

                    b.Navigation("DisplayName");
                });

            modelBuilder.Entity("VRCModding.Entities.AccountInfo", b =>
                {
                    b.Navigation("DisplayNameHistory");
                });

            modelBuilder.Entity("VRCModding.Entities.DisplayName", b =>
                {
                    b.Navigation("CurrentAccount");

                    b.Navigation("HasBeenUsedBy");
                });

            modelBuilder.Entity("VRCModding.Entities.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("KnownHWIDs");

                    b.Navigation("KnownIPs");
                });
#pragma warning restore 612, 618
        }
    }
}
