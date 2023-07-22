﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hasbiHAliCia.Veri;

#nullable disable

namespace hasbiHAliCia.Migrations
{
    [DbContext(typeof(VeritabaniBaglami))]
    [Migration("20230721202009_Goc1")]
    partial class Goc1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("hasbiHAliCia.Veri.Varlik.VIleti", b =>
                {
                    b.Property<int>("Kimlik")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Kimlik"));

                    b.Property<DateTime>("GonderilmeZamani")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icerik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("KullaniciK")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Kimlik");

                    b.ToTable("VIleti", t =>
                        {
                            t.HasTrigger("SomeTrigger");
                        });
                });

            modelBuilder.Entity("hasbiHAliCia.Veri.Varlik.VKullanici", b =>
                {
                    b.Property<Guid>("Kimlik")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("GirisZamani")
                        .HasColumnType("datetime2");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Kimlik");

                    b.ToTable("Kullanicilar");
                });
#pragma warning restore 612, 618
        }
    }
}
