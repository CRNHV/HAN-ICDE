﻿// <auto-generated />
using System;
using ICDE.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ICDE.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241124221436_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CursusVak", b =>
                {
                    b.Property<int>("CursussenId")
                        .HasColumnType("int");

                    b.Property<int>("VakId")
                        .HasColumnType("int");

                    b.HasKey("CursussenId", "VakId");

                    b.HasIndex("VakId");

                    b.ToTable("VakCursussenLeeruitkomsten", (string)null);
                });

            modelBuilder.Entity("ICDE.Data.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("ICDE.Data.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.BeoordelingCriterea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpdrachtId")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpdrachtId");

                    b.ToTable("BeoordelingCritereas");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Cursus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanningId")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanningId");

                    b.ToTable("Cursussen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Leeruitkomst", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BeoordelingCritereaId")
                        .HasColumnType("int");

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BeoordelingCritereaId");

                    b.ToTable("leeruitkomstsen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Les", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lessen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Opleiding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Opleidingen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Planning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plannings");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.PlanningItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Index")
                        .HasColumnType("int");

                    b.Property<int?>("LesId")
                        .HasColumnType("int");

                    b.Property<int?>("OpdrachtId")
                        .HasColumnType("int");

                    b.Property<int>("PlanningId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LesId");

                    b.HasIndex("OpdrachtId");

                    b.HasIndex("PlanningId");

                    b.ToTable("PlanningItems");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Vak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OpleidingId")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpleidingId");

                    b.ToTable("Vakken");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.IngeleverdeOpdracht", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpdrachtId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpdrachtId");

                    b.ToTable("IngeleverdeOpdrachten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.Opdracht", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Opdrachten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.OpdrachtBeoordeling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cijfer")
                        .HasColumnType("float");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IngeleverdeOpdrachtId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IngeleverdeOpdrachtId");

                    b.ToTable("OpdrachtBeoordelingen");
                });

            modelBuilder.Entity("LeeruitkomstLes", b =>
                {
                    b.Property<int>("LeeruitkomstenId")
                        .HasColumnType("int");

                    b.Property<int>("LesId")
                        .HasColumnType("int");

                    b.HasKey("LeeruitkomstenId", "LesId");

                    b.HasIndex("LesId");

                    b.ToTable("LesLeeruitkomsten", (string)null);
                });

            modelBuilder.Entity("LeeruitkomstVak", b =>
                {
                    b.Property<int>("LeeruitkomstenId")
                        .HasColumnType("int");

                    b.Property<int>("VakId")
                        .HasColumnType("int");

                    b.HasKey("LeeruitkomstenId", "VakId");

                    b.HasIndex("VakId");

                    b.ToTable("VakLeeruitkomsten", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CursusVak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Cursus", null)
                        .WithMany()
                        .HasForeignKey("CursussenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Vak", null)
                        .WithMany()
                        .HasForeignKey("VakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.BeoordelingCriterea", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Opdrachten.Opdracht", null)
                        .WithMany("BeoordelingCritereas")
                        .HasForeignKey("OpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Cursus", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Planning", "Planning")
                        .WithMany()
                        .HasForeignKey("PlanningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Leeruitkomst", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.BeoordelingCriterea", null)
                        .WithMany("Leeruitkomsten")
                        .HasForeignKey("BeoordelingCritereaId");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.PlanningItem", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Les", "Les")
                        .WithMany()
                        .HasForeignKey("LesId");

                    b.HasOne("ICDE.Data.Entities.Opdrachten.Opdracht", "Opdracht")
                        .WithMany()
                        .HasForeignKey("OpdrachtId");

                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Planning", "Planning")
                        .WithMany("PlanningItems")
                        .HasForeignKey("PlanningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Les");

                    b.Navigation("Opdracht");

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Vak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Opleiding", null)
                        .WithMany("Vakken")
                        .HasForeignKey("OpleidingId");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.IngeleverdeOpdracht", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Opdrachten.Opdracht", "Opdracht")
                        .WithMany("IngeleverdeOpdrachten")
                        .HasForeignKey("OpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Opdracht");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.OpdrachtBeoordeling", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Opdrachten.IngeleverdeOpdracht", "IngeleverdeOpdracht")
                        .WithMany("Beoordelingen")
                        .HasForeignKey("IngeleverdeOpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngeleverdeOpdracht");
                });

            modelBuilder.Entity("LeeruitkomstLes", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Leeruitkomst", null)
                        .WithMany()
                        .HasForeignKey("LeeruitkomstenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Les", null)
                        .WithMany()
                        .HasForeignKey("LesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeeruitkomstVak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Leeruitkomst", null)
                        .WithMany()
                        .HasForeignKey("LeeruitkomstenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.OnderwijsOnderdeel.Vak", null)
                        .WithMany()
                        .HasForeignKey("VakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.BeoordelingCriterea", b =>
                {
                    b.Navigation("Leeruitkomsten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Opleiding", b =>
                {
                    b.Navigation("Vakken");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OnderwijsOnderdeel.Planning", b =>
                {
                    b.Navigation("PlanningItems");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.IngeleverdeOpdracht", b =>
                {
                    b.Navigation("Beoordelingen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdrachten.Opdracht", b =>
                {
                    b.Navigation("BeoordelingCritereas");

                    b.Navigation("IngeleverdeOpdrachten");
                });
#pragma warning restore 612, 618
        }
    }
}
