﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrioniaApp.Database;

#nullable disable

namespace PrioniaApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230123111325_ProductTags")]
    partial class ProductTags
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrioniaApp.Database.Models.Catagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Catagoryies", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Colors", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Navbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Navbars", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.PaymentBenefit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Backgroundİmage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundİmageInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("PaymentBenefits", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Rate")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductCatagory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CatagoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CatagoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCatagoryies", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColors", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageNameInFileSystem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImagees", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("ProductSizes", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("TagId");

                    b.ToTable("ProductTags", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sizes", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Slider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Backgroundİmage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BackgroundİmageInFileSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Button")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ButtonRedirectUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("HeaderTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Sliders", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.SubNavbar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NavbarId")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("ToURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NavbarId");

                    b.ToTable("SubNavbars", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Catagory", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Catagory", "Parent")
                        .WithMany("Catagories")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductCatagory", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Catagory", "Catagory")
                        .WithMany("ProductCatagories")
                        .HasForeignKey("CatagoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrioniaApp.Database.Models.Product", "Product")
                        .WithMany("ProductCatagories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catagory");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductColor", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Color", "Color")
                        .WithMany("ProductColors")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrioniaApp.Database.Models.Product", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductImage", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductSize", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrioniaApp.Database.Models.Size", "Size")
                        .WithMany("ProductSizes")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.ProductTag", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrioniaApp.Database.Models.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.SubNavbar", b =>
                {
                    b.HasOne("PrioniaApp.Database.Models.Navbar", "Navbar")
                        .WithMany("SubNavbars")
                        .HasForeignKey("NavbarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Navbar");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Catagory", b =>
                {
                    b.Navigation("Catagories");

                    b.Navigation("ProductCatagories");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Color", b =>
                {
                    b.Navigation("ProductColors");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Navbar", b =>
                {
                    b.Navigation("SubNavbars");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Product", b =>
                {
                    b.Navigation("ProductCatagories");

                    b.Navigation("ProductColors");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductSizes");

                    b.Navigation("ProductTags");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Size", b =>
                {
                    b.Navigation("ProductSizes");
                });

            modelBuilder.Entity("PrioniaApp.Database.Models.Tag", b =>
                {
                    b.Navigation("ProductTags");
                });
#pragma warning restore 612, 618
        }
    }
}
