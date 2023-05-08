using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class ResellerImportReceiptDetails
	{
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int Price { get; set; }
		public int ResellerImportReceiptId { get; set; }
		public ResellerImportReceipt ResellerImportReceipt { get; set; }
		public int PhoneId { get; set; }
		public Phone Phone { get; set; }
		public int WarehouseId { get; set; }
		public Warehouse Warehouse { get; set;}
	}
}
