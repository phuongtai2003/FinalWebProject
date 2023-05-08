using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class FinalDbContext : DbContext
	{
		public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ReceiptDetails>().HasKey(rd => new {rd.ReceiptId, rd.PhoneId});
			modelBuilder.Entity<WarehouseProducts>().HasKey(wp => new { wp.WarehouseId, wp.PhoneId });
			modelBuilder.Entity<ResellerImportReceipt>().Property(ric => ric.PaymentStatus).HasDefaultValue(0);
			modelBuilder.Entity<ResellerImportReceiptDetails>().HasKey(ricd => new { ricd.PhoneId, ricd.ResellerImportReceiptId, ricd.WarehouseId});
			modelBuilder.Entity<ExportReceiptDetails>().HasKey(erd => new { erd.PhoneId, erd.ResellerId, erd.ExportReceiptId});
			modelBuilder.Entity<ResellerStorage>().HasKey(rs => new {rs.PhoneId, rs.ResellerId});
		}
		public DbSet<Accountant> Accountant { get; set; }
		public DbSet<Warehouse> Warehouse { get; set; }
		public DbSet<Manufacturer> Manufacturer { get; set; }
		public DbSet<Phone> Phone { get; set; }
		public DbSet<Receipt> Receipt { get; set; }
		public DbSet<ReceiptDetails> ReceiptDetails { get; set; }
		public DbSet<WarehouseProducts> WarehouseProducts { get; set;}
		public DbSet<Reseller> Reseller { get; set; }
		public DbSet<ResellerImportReceipt> ResellerImportReceipt { get; set; }
		public DbSet<ResellerImportReceiptDetails> ResellerImportReceiptDetail { get; set; }
		public DbSet<DeliveryStatus> DeliveryStatus { get; set; }
		public DbSet<ResellerStorage> ResellerStorage { get; set; }
		public DbSet<ExportReceipt> ExportReceipt { get; set; }
		public DbSet<ExportReceiptDetails> ExportReceiptDetails { get; set; }
	}
}
