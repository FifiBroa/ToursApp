﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToursApp.Models;

namespace ToursApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToursApp.Models.Country", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("ToursApp.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountOfStars")
                        .HasColumnType("int");

                    b.Property<int?>("CountryCode")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryCode");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("ToursApp.Models.HotelComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CratiopnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("hotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("hotelId");

                    b.ToTable("HotelComments");
                });

            modelBuilder.Entity("ToursApp.Models.HotelImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<byte>("ImageSource")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("ToursApp.Models.HotelOfTour", b =>
                {
                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("TourId", "HotelId");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelOfTours");
                });

            modelBuilder.Entity("ToursApp.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePreview")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool>("IsActual")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TicketCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("ToursApp.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("ToursApp.Models.TypeOfTour", b =>
                {
                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("TourId", "TypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("TypeOfTours");
                });

            modelBuilder.Entity("ToursApp.Models.Hotel", b =>
                {
                    b.HasOne("ToursApp.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryCode");
                });

            modelBuilder.Entity("ToursApp.Models.HotelComment", b =>
                {
                    b.HasOne("ToursApp.Models.Hotel", "hotel")
                        .WithMany()
                        .HasForeignKey("hotelId");
                });

            modelBuilder.Entity("ToursApp.Models.HotelImage", b =>
                {
                    b.HasOne("ToursApp.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("ToursApp.Models.HotelOfTour", b =>
                {
                    b.HasOne("ToursApp.Models.Hotel", "Hotel")
                        .WithMany("HotelOfTours")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToursApp.Models.Tour", "Tour")
                        .WithMany("HotelOfTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToursApp.Models.TypeOfTour", b =>
                {
                    b.HasOne("ToursApp.Models.Tour", "Tour")
                        .WithMany("TypeOfTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToursApp.Models.Type", "Type")
                        .WithMany("TypeOfTours")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}