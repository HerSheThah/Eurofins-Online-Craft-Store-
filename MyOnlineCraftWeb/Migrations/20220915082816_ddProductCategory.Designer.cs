﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyOnlineCraftWeb;

#nullable disable

namespace MyOnlineCraftWeb.Migrations
{
    [DbContext(typeof(OnlineCraftStoreDbContext))]
    [Migration("20220915082816_ddProductCategory")]
    partial class ddProductCategory
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyOnlineCraftWeb.Models.Category", b =>
                {
                    b.Property<int>("catId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("catId"), 1L, 1);

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("catId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MyOnlineCraftWeb.Models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productId"), 1L, 1);

                    b.Property<float>("ActualPrice")
                        .HasColumnType("real");

                    b.Property<float>("DiscountPrice")
                        .HasColumnType("real");

                    b.Property<string>("imageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productCategoryIdcatId")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("productId");

                    b.HasIndex("productCategoryIdcatId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MyOnlineCraftWeb.Models.Product", b =>
                {
                    b.HasOne("MyOnlineCraftWeb.Models.Category", "productCategoryId")
                        .WithMany()
                        .HasForeignKey("productCategoryIdcatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("productCategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}