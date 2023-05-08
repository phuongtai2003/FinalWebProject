using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Accountant
	{
		[Key]
		public int AccountantID { get; set; }
		[Required]
		public string AcccountantName { get; set; }
		[Required]
		public string AccountantEmail { get; set; }
		[Required]
		public string AccountantPassword { get; set;}
		[Required]
		public int WarehouseID { get; set; }
		public Warehouse Warehouse { get; set;}
		public virtual ICollection<Receipt> Receipts { get; set; }
		public virtual ICollection<ExportReceipt> ExportReceipts { get; set; }
	}
}
