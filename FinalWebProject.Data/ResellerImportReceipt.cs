using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class ResellerImportReceipt
	{
		[Key]
		public int ResellerImportReceiptId { get; set; }
		[Required]
		public int TotalPrice { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }
		[Required]
		public string PaymentMethod { get; set; }
		[Required]
		public int PaymentStatus { get; set; }

		public int DeliveryStatusId { get; set; }
		public DeliveryStatus DeliveryStatus { get; set; }
		public int ResellerId { get; set; }
		public Reseller Reseller { get; set; }
		public virtual ICollection<ResellerImportReceiptDetails> ResellerImportReceiptDetails { get; set; }
	}
}
