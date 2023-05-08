using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Reseller
	{
		[Key]
		public int ResellerId { get; set; }
		[Required]
		public string ResellerName { get; set; }
		[Required]
		[EmailAddress]
		public string ResellerEmail { get; set; }
		[Required]
		public string ResellerPassword { get; set; }
		[Required]
		public string ResellerLocation { get; set; }
		public virtual ICollection<ExportReceiptDetails> ExportReceiptDetails { get; set; }
		public virtual ICollection<ResellerImportReceipt> ResellerImportReceipts { get; set; }
		public virtual ICollection<ResellerStorage> ResellerStorage { get; set; }

	}
}
