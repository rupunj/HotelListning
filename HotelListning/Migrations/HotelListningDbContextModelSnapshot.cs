﻿// <auto-generated />
using HotelListning.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelListning.Migrations
{
    [DbContext(typeof(HotelListningDbContext))]
    partial class HotelListningDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelListning.Data.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sri Lanka",
                            ShortName = "LK"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Bahamas",
                            ShortName = "BS"
                        },
                        new
                        {
                            Id = 3,
                            Name = "India",
                            ShortName = "IN"
                        });
                });

            modelBuilder.Entity("HotelListning.Data.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Negambo",
                            CountryId = 1,
                            Name = "Avenra",
                            Rating = 4.2999999999999998
                        },
                        new
                        {
                            Id = 2,
                            Address = "Chennai",
                            CountryId = 3,
                            Name = "Hotel India",
                            Rating = 4.7999999999999998
                        },
                        new
                        {
                            Id = 3,
                            Address = "Bahamas Town",
                            CountryId = 2,
                            Name = "Hotel Bahamas",
                            Rating = 4.7999999999999998
                        });
                });

            modelBuilder.Entity("HotelListning.Data.Hotel", b =>
                {
                    b.HasOne("HotelListning.Data.Country", "Country")
                        .WithMany("Hotels")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("HotelListning.Data.Country", b =>
                {
                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
