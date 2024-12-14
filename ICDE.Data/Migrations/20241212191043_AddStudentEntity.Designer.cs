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
    [Migration("20241212191043_AddStudentEntity")]
    partial class AddStudentEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BeoordelingCritereaLeeruitkomst", b =>
                {
                    b.Property<int>("BeoordelingCritereaId")
                        .HasColumnType("int");

                    b.Property<int>("LeeruitkomstenId")
                        .HasColumnType("int");

                    b.HasKey("BeoordelingCritereaId", "LeeruitkomstenId");

                    b.HasIndex("LeeruitkomstenId");

                    b.ToTable("BeoordelingCritereaLeeruitkomst");
                });

            modelBuilder.Entity("BeoordelingCritereaOpdracht", b =>
                {
                    b.Property<int>("BeoordelingCritereasId")
                        .HasColumnType("int");

                    b.Property<int>("OpdrachtId")
                        .HasColumnType("int");

                    b.HasKey("BeoordelingCritereasId", "OpdrachtId");

                    b.HasIndex("OpdrachtId");

                    b.ToTable("BeoordelingCritereaOpdracht");
                });

            modelBuilder.Entity("CursusVak", b =>
                {
                    b.Property<int>("CursussenId")
                        .HasColumnType("int");

                    b.Property<int>("VakId")
                        .HasColumnType("int");

                    b.HasKey("CursussenId", "VakId");

                    b.HasIndex("VakId");

                    b.ToTable("VakCursussen", (string)null);
                });

            modelBuilder.Entity("ICDE.Data.Entities.BeoordelingCriterea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OpdrachtId")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BeoordelingCritereas");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Cursus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlanningId")
                        .HasColumnType("int");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanningId");

                    b.ToTable("Cursussen");
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

            modelBuilder.Entity("ICDE.Data.Entities.IngeleverdeOpdracht", b =>
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

                    b.Property<int?>("StudentNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OpdrachtId");

                    b.HasIndex("StudentNummer");

                    b.ToTable("IngeleverdeOpdrachten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Leeruitkomst", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CursusId")
                        .HasColumnType("int");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CursusId");

                    b.ToTable("leeruitkomstsen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Les", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Lessen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdracht", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("ICDE.Data.Entities.OpdrachtBeoordeling", b =>
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

            modelBuilder.Entity("ICDE.Data.Entities.Opleiding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Opleidingen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Planning", b =>
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

            modelBuilder.Entity("ICDE.Data.Entities.PlanningItem", b =>
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

            modelBuilder.Entity("ICDE.Data.Entities.Student", b =>
                {
                    b.Property<int>("StudentNummer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentNummer"));

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StudentNummer");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Studenten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Vak", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersieNummer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vakken");
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

            modelBuilder.Entity("OpleidingVak", b =>
                {
                    b.Property<int>("OpleidingId")
                        .HasColumnType("int");

                    b.Property<int>("VakkenId")
                        .HasColumnType("int");

                    b.HasKey("OpleidingId", "VakkenId");

                    b.HasIndex("VakkenId");

                    b.ToTable("OpleidingVak");
                });

            modelBuilder.Entity("BeoordelingCritereaLeeruitkomst", b =>
                {
                    b.HasOne("ICDE.Data.Entities.BeoordelingCriterea", null)
                        .WithMany()
                        .HasForeignKey("BeoordelingCritereaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Leeruitkomst", null)
                        .WithMany()
                        .HasForeignKey("LeeruitkomstenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BeoordelingCritereaOpdracht", b =>
                {
                    b.HasOne("ICDE.Data.Entities.BeoordelingCriterea", null)
                        .WithMany()
                        .HasForeignKey("BeoordelingCritereasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Opdracht", null)
                        .WithMany()
                        .HasForeignKey("OpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CursusVak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Cursus", null)
                        .WithMany()
                        .HasForeignKey("CursussenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Vak", null)
                        .WithMany()
                        .HasForeignKey("VakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICDE.Data.Entities.Cursus", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Planning", "Planning")
                        .WithMany()
                        .HasForeignKey("PlanningId");

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("ICDE.Data.Entities.IngeleverdeOpdracht", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Opdracht", "Opdracht")
                        .WithMany("IngeleverdeOpdrachten")
                        .HasForeignKey("OpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Student", null)
                        .WithMany("IngeleverdeOpdrachten")
                        .HasForeignKey("StudentNummer");

                    b.Navigation("Opdracht");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Leeruitkomst", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Cursus", null)
                        .WithMany("Leeruitkomsten")
                        .HasForeignKey("CursusId");
                });

            modelBuilder.Entity("ICDE.Data.Entities.OpdrachtBeoordeling", b =>
                {
                    b.HasOne("ICDE.Data.Entities.IngeleverdeOpdracht", "IngeleverdeOpdracht")
                        .WithMany("Beoordelingen")
                        .HasForeignKey("IngeleverdeOpdrachtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngeleverdeOpdracht");
                });

            modelBuilder.Entity("ICDE.Data.Entities.PlanningItem", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Les", "Les")
                        .WithMany()
                        .HasForeignKey("LesId");

                    b.HasOne("ICDE.Data.Entities.Opdracht", "Opdracht")
                        .WithMany()
                        .HasForeignKey("OpdrachtId");

                    b.HasOne("ICDE.Data.Entities.Planning", "Planning")
                        .WithMany("PlanningItems")
                        .HasForeignKey("PlanningId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Les");

                    b.Navigation("Opdracht");

                    b.Navigation("Planning");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Student", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Identity.User", "User")
                        .WithOne()
                        .HasForeignKey("ICDE.Data.Entities.Student", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LeeruitkomstLes", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Leeruitkomst", null)
                        .WithMany()
                        .HasForeignKey("LeeruitkomstenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Les", null)
                        .WithMany()
                        .HasForeignKey("LesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeeruitkomstVak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Leeruitkomst", null)
                        .WithMany()
                        .HasForeignKey("LeeruitkomstenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Vak", null)
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

            modelBuilder.Entity("OpleidingVak", b =>
                {
                    b.HasOne("ICDE.Data.Entities.Opleiding", null)
                        .WithMany()
                        .HasForeignKey("OpleidingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICDE.Data.Entities.Vak", null)
                        .WithMany()
                        .HasForeignKey("VakkenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICDE.Data.Entities.Cursus", b =>
                {
                    b.Navigation("Leeruitkomsten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.IngeleverdeOpdracht", b =>
                {
                    b.Navigation("Beoordelingen");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Opdracht", b =>
                {
                    b.Navigation("IngeleverdeOpdrachten");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Planning", b =>
                {
                    b.Navigation("PlanningItems");
                });

            modelBuilder.Entity("ICDE.Data.Entities.Student", b =>
                {
                    b.Navigation("IngeleverdeOpdrachten");
                });
#pragma warning restore 612, 618
        }
    }
}
