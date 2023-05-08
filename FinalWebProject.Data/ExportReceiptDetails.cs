using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class ExportReceiptDetails
	{
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int Price { get; set; }
		public int ResellerId { get; set; }
		public Reseller Reseller { get; set; }
		public int PhoneId { get; set; }
		public Phone Phone { get; set; }
		public int ExportReceiptId { get; set; }
		public ExportReceipt ExportReceipt { get; set; }
	}
}
