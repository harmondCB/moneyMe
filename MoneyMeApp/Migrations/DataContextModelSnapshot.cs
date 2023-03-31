﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyMeApp.Data;

namespace MoneyMeApp.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MoneyMeApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MoneyMeApp.Models.CustomerPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPayments");
                });

            modelBuilder.Entity("MoneyMeApp.Models.CustomerPaymentProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerPaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Interest")
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerPaymentId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("CustomerPaymentProducts");
                });

            modelBuilder.Entity("MoneyMeApp.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InterestFreeMonth")
                        .HasColumnType("int");

                    b.Property<decimal>("InterestRate")
                        .HasColumnType("decimal(19,4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InterestFreeMonth = 0,
                            InterestRate = 0m,
                            Name = "ProductA"
                        },
                        new
                        {
                            Id = 2,
                            InterestFreeMonth = 2,
                            InterestRate = 0.05m,
                            Name = "ProductB"
                        },
                        new
                        {
                            Id = 3,
                            InterestFreeMonth = 0,
                            InterestRate = 0.05m,
                            Name = "ProductC"
                        });
                });

            modelBuilder.Entity("MoneyMeApp.Models.CustomerPayment", b =>
                {
                    b.HasOne("MoneyMeApp.Models.Customer", "Customer")
                        .WithMany("CustomerPayments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("MoneyMeApp.Models.CustomerPaymentProduct", b =>
                {
                    b.HasOne("MoneyMeApp.Models.CustomerPayment", "CustomerPayment")
                        .WithOne("CustomerPaymentProduct")
                        .HasForeignKey("MoneyMeApp.Models.CustomerPaymentProduct", "CustomerPaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyMeApp.Models.Product", "Product")
                        .WithMany("CustomerPaymentProduct")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerPayment");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("MoneyMeApp.Models.Customer", b =>
                {
                    b.Navigation("CustomerPayments");
                });

            modelBuilder.Entity("MoneyMeApp.Models.CustomerPayment", b =>
                {
                    b.Navigation("CustomerPaymentProduct");
                });

            modelBuilder.Entity("MoneyMeApp.Models.Product", b =>
                {
                    b.Navigation("CustomerPaymentProduct");
                });
#pragma warning restore 612, 618
        }
    }
}
