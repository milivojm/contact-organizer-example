﻿// <auto-generated />
using System;
using ContactOrganizer.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactOrganizer.Data.SqlServer.Migrations
{
    [DbContext(typeof(ContactOrganizerSqlRepository))]
    [Migration("20181219220803_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactOrganizer.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TelephoneNumber")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName")
                        .IsUnique();

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("ContactOrganizer.Contact", b =>
                {
                    b.OwnsOne("ContactOrganizer.ContactAddress", "_contactAddress", b1 =>
                        {
                            b1.Property<Guid>("ContactId");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnName("City")
                                .HasMaxLength(40);

                            b1.Property<string>("Country")
                                .HasColumnName("Country")
                                .HasMaxLength(50);

                            b1.Property<string>("PostalCode")
                                .HasColumnName("PostalCode")
                                .HasMaxLength(20);

                            b1.Property<string>("StreetAndNumber")
                                .IsRequired()
                                .HasColumnName("StreetNumber")
                                .HasMaxLength(80);

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.HasOne("ContactOrganizer.Contact")
                                .WithOne("_contactAddress")
                                .HasForeignKey("ContactOrganizer.ContactAddress", "ContactId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
