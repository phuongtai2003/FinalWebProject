﻿// <auto-generated />
using System;
using FinalWebProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalWebProject.Data.Migrations
{
    [DbContext(typeof(FinalDbContext))]
    partial class FinalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalWebProject.Data.Accountant", b =>
                {
                    b.Property<int>("AccountantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountantID"));

                    b.Property<string>("AcccountantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountantEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccountantPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarehouseID")
                        .HasColumnType("int");

                    b.HasKey("AccountantID");

                    b.HasIndex("WarehouseID");

                    b.ToTable("Accountant");
                });

            modelBuilder.Entity("FinalWebProject.Data.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("FinalWebProject.Data.DeliveryStatus", b =>
                {
                    b.Property<int>("DeliveryStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryStatusId"));

                    b.Property<string>("DeliveryStatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DeliveryStatusId");

                    b.ToTable("DeliveryStatus");
                });

            modelBuilder.Entity("FinalWebProject.Data.ExportReceipt", b =>
                {
                    b.Property<int>("ExportReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExportReceiptId"));

                    b.Property<int>("AccountantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("ExportReceiptId");

                    b.HasIndex("AccountantId");

                    b.ToTable("ExportReceipt");
                });

            modelBuilder.Entity("FinalWebProject.Data.ExportReceiptDetails", b =>
                {
                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("ResellerId")
                        .HasColumnType("int");

                    b.Property<int>("ExportReceiptId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PhoneId", "ResellerId", "ExportReceiptId");

                    b.HasIndex("ExportReceiptId");

                    b.HasIndex("ResellerId");

                    b.ToTable("ExportReceiptDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManufacturerId"));

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerYear")
                        .HasColumnType("int");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturer");
                });

            modelBuilder.Entity("FinalWebProject.Data.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("FinalWebProject.Data.OrderDetails", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "PhoneId");

                    b.HasIndex("PhoneId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Phone", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneId"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneYear")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("PhoneId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("FinalWebProject.Data.Rating", b =>
                {
                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("ReviewRating")
                        .HasColumnType("float");

                    b.HasKey("PhoneId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("FinalWebProject.Data.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptId"));

                    b.Property<int>("AccountantId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("ReceiptId");

                    b.HasIndex("AccountantId");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("FinalWebProject.Data.ReceiptDetails", b =>
                {
                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ReceiptId", "PhoneId");

                    b.HasIndex("PhoneId");

                    b.ToTable("ReceiptDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Reseller", b =>
                {
                    b.Property<int>("ResellerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResellerId"));

                    b.Property<string>("ResellerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResellerLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResellerPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ResellerId");

                    b.ToTable("Reseller");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerImportReceipt", b =>
                {
                    b.Property<int>("ResellerImportReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResellerImportReceiptId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeliveryStatusId")
                        .HasColumnType("int");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("ResellerId")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("ResellerImportReceiptId");

                    b.HasIndex("DeliveryStatusId");

                    b.HasIndex("ResellerId");

                    b.ToTable("ResellerImportReceipt");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerImportReceiptDetails", b =>
                {
                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("ResellerImportReceiptId")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PhoneId", "ResellerImportReceiptId", "WarehouseId");

                    b.HasIndex("ResellerImportReceiptId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ResellerImportReceiptDetail");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerStorage", b =>
                {
                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("ResellerId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PhoneId", "ResellerId");

                    b.HasIndex("ResellerId");

                    b.ToTable("ResellerStorage");
                });

            modelBuilder.Entity("FinalWebProject.Data.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"));

                    b.Property<string>("WarehouseLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("FinalWebProject.Data.WarehouseProducts", b =>
                {
                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId", "PhoneId");

                    b.HasIndex("PhoneId");

                    b.ToTable("WarehouseProducts");
                });

            modelBuilder.Entity("FinalWebProject.Data.Accountant", b =>
                {
                    b.HasOne("FinalWebProject.Data.Warehouse", "Warehouse")
                        .WithMany("Accountants")
                        .HasForeignKey("WarehouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("FinalWebProject.Data.ExportReceipt", b =>
                {
                    b.HasOne("FinalWebProject.Data.Accountant", "Accountant")
                        .WithMany("ExportReceipts")
                        .HasForeignKey("AccountantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accountant");
                });

            modelBuilder.Entity("FinalWebProject.Data.ExportReceiptDetails", b =>
                {
                    b.HasOne("FinalWebProject.Data.ExportReceipt", "ExportReceipt")
                        .WithMany("ExportReceiptDetails")
                        .HasForeignKey("ExportReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("ExportReceiptDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Reseller", "Reseller")
                        .WithMany("ExportReceiptDetails")
                        .HasForeignKey("ResellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExportReceipt");

                    b.Navigation("Phone");

                    b.Navigation("Reseller");
                });

            modelBuilder.Entity("FinalWebProject.Data.Order", b =>
                {
                    b.HasOne("FinalWebProject.Data.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("FinalWebProject.Data.OrderDetails", b =>
                {
                    b.HasOne("FinalWebProject.Data.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("OrderDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("FinalWebProject.Data.Phone", b =>
                {
                    b.HasOne("FinalWebProject.Data.Manufacturer", "Manufacturer")
                        .WithMany("Phones")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("FinalWebProject.Data.Rating", b =>
                {
                    b.HasOne("FinalWebProject.Data.Customer", "Customer")
                        .WithMany("Ratings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("Ratings")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("FinalWebProject.Data.Receipt", b =>
                {
                    b.HasOne("FinalWebProject.Data.Accountant", "Accountant")
                        .WithMany("Receipts")
                        .HasForeignKey("AccountantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accountant");
                });

            modelBuilder.Entity("FinalWebProject.Data.ReceiptDetails", b =>
                {
                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Receipt", "Receipt")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerImportReceipt", b =>
                {
                    b.HasOne("FinalWebProject.Data.DeliveryStatus", "DeliveryStatus")
                        .WithMany("ResellerImportReceipts")
                        .HasForeignKey("DeliveryStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Reseller", "Reseller")
                        .WithMany("ResellerImportReceipts")
                        .HasForeignKey("ResellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryStatus");

                    b.Navigation("Reseller");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerImportReceiptDetails", b =>
                {
                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("ResellerImportReceiptDetails")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.ResellerImportReceipt", "ResellerImportReceipt")
                        .WithMany("ResellerImportReceiptDetails")
                        .HasForeignKey("ResellerImportReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Warehouse", "Warehouse")
                        .WithMany("ResellerImportReceiptDetails")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");

                    b.Navigation("ResellerImportReceipt");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerStorage", b =>
                {
                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("ResellerStorage")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Reseller", "Reseller")
                        .WithMany("ResellerStorage")
                        .HasForeignKey("ResellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");

                    b.Navigation("Reseller");
                });

            modelBuilder.Entity("FinalWebProject.Data.WarehouseProducts", b =>
                {
                    b.HasOne("FinalWebProject.Data.Phone", "Phone")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWebProject.Data.Warehouse", "Warehouse")
                        .WithMany("WarehouseProducts")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Phone");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("FinalWebProject.Data.Accountant", b =>
                {
                    b.Navigation("ExportReceipts");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("FinalWebProject.Data.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("FinalWebProject.Data.DeliveryStatus", b =>
                {
                    b.Navigation("ResellerImportReceipts");
                });

            modelBuilder.Entity("FinalWebProject.Data.ExportReceipt", b =>
                {
                    b.Navigation("ExportReceiptDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Manufacturer", b =>
                {
                    b.Navigation("Phones");
                });

            modelBuilder.Entity("FinalWebProject.Data.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Phone", b =>
                {
                    b.Navigation("ExportReceiptDetails");

                    b.Navigation("OrderDetails");

                    b.Navigation("Ratings");

                    b.Navigation("ReceiptDetails");

                    b.Navigation("ResellerImportReceiptDetails");

                    b.Navigation("ResellerStorage");

                    b.Navigation("WarehouseProducts");
                });

            modelBuilder.Entity("FinalWebProject.Data.Receipt", b =>
                {
                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Reseller", b =>
                {
                    b.Navigation("ExportReceiptDetails");

                    b.Navigation("ResellerImportReceipts");

                    b.Navigation("ResellerStorage");
                });

            modelBuilder.Entity("FinalWebProject.Data.ResellerImportReceipt", b =>
                {
                    b.Navigation("ResellerImportReceiptDetails");
                });

            modelBuilder.Entity("FinalWebProject.Data.Warehouse", b =>
                {
                    b.Navigation("Accountants");

                    b.Navigation("ResellerImportReceiptDetails");

                    b.Navigation("WarehouseProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
