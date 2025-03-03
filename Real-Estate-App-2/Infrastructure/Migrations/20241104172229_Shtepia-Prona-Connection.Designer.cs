﻿// <auto-generated />
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WebUI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241104172229_Shtepia-Prona-Connection")]
    partial class ShtepiaPronaConnection
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Prona", b =>
                {
                    b.Property<int>("PronaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PronaID"));

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PronaID");

                    b.ToTable("Pronas", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.Apartment", b =>
                {
                    b.HasBaseType("Domain.Entities.Prona");

                    b.Property<int>("floor")
                        .HasColumnType("int");

                    b.Property<bool>("kaAnshensor")
                        .HasColumnType("bit");

                    b.Property<int>("nrDhomave")
                        .HasColumnType("int");

                    b.ToTable("Apartments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Shtepia", b =>
                {
                    b.HasBaseType("Domain.Entities.Prona");

                    b.Property<bool>("kaGarazhd")
                        .HasColumnType("bit");

                    b.Property<bool>("kaPool")
                        .HasColumnType("bit");

                    b.Property<int>("nrFloors")
                        .HasColumnType("int");

                    b.Property<double>("size")
                        .HasColumnType("float");

                    b.ToTable("Shtepiat", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Toka", b =>
                {
                    b.HasBaseType("Domain.Entities.Prona");

                    b.Property<string>("LandType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopografiaTokes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("WaterSource")
                        .HasColumnType("bit");

                    b.Property<string>("Zona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Tokat", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Apartment", b =>
                {
                    b.HasOne("Domain.Entities.Prona", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Apartment", "PronaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Shtepia", b =>
                {
                    b.HasOne("Domain.Entities.Prona", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Shtepia", "PronaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Toka", b =>
                {
                    b.HasOne("Domain.Entities.Prona", null)
                        .WithOne()
                        .HasForeignKey("Domain.Entities.Toka", "PronaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
