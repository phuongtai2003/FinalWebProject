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
		}
		public DbSet<Accountant> Accountant { get; set; }
		public DbSet<Warehouse> Warehouse { get; set; }
		public DbSet<Manufacturer> Manufacturer { get; set; }
		public DbSet<Phone> Phone { get; set; }
		public DbSet<Receipt> Receipt { get; set; }
		public DbSet<ReceiptDetails> ReceiptDetails { get; set; }
		public DbSet<WarehouseProducts> WarehouseProducts { get; set;}
		public DbSet<Reseller> Reseller { get; set; }
	}
}
