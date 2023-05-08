using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Warehouse
	{
		[Key]
		public int WarehouseId { get; set; }
		[Required]
		public string WarehouseName { get; set; }
		[Required]
		public string WarehouseLocation { get; set; }
		public virtual ICollection<Accountant> Accountants { get; set; }
		public virtual ICollection<WarehouseProducts> WarehouseProducts { get; set; }
		public virtual ICollection<ResellerImportReceiptDetails> ResellerImportReceiptDetails { get;set; }
	}
}
