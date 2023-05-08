using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class ExportReceipt
	{
		[Key]
		public int ExportReceiptId { get; set; }
		[Required]
		public int TotalPrice { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }
		public int AccountantId { get; set; }
		public Accountant Accountant { get; set; }
		public virtual ICollection<ExportReceiptDetails> ExportReceiptDetails { get; set; }
	}
}
