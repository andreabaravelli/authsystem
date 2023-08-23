﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SeatsProject.Models;

#nullable disable

namespace SeatsProject.Migrations
{
    [DbContext(typeof(SeatsProjectContext))]
    [Migration("20230809094012_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SeatsProject.Models.Sede", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodiceArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPadre")
                        .HasColumnType("int");

                    b.Property<bool>("Prenotabile")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Sedi");
                });
#pragma warning restore 612, 618
        }
    }
}
