using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Receipt
	{
		[Key]
		public int ReceiptId { get; set; }
		[Required]
		public int TotalPrice { get; set; }
		[Required]
		public DateTime DateCreated { get; set; }
		public int AccountantId { get; set; }
		public Accountant Accountant { get; set; }
		public virtual ICollection<ReceiptDetails> ReceiptDetails { get; set; }
	}
}
