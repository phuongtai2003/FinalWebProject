using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Phone
	{
		[Key]
		public int PhoneId { get; set; }
		[Required]
		public string PhoneName { get; set; }
		[Required]
		public string PhoneDescription { get; set; }
		[Required]
		[Range(1990, 2023, ErrorMessage ="Invalid Released Year")]
		public int PhoneYear { get; set; }
		[Required]
		public string Image { get; set; }
		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Invalid Price Value")]
		public int Price { get; set; }
		public int ManufacturerId { get; set; }
		public Manufacturer Manufacturer { get; set; }
		public virtual ICollection<ReceiptDetails> ReceiptDetails { get; set; }
		public virtual ICollection<WarehouseProducts> WarehouseProducts { get; set;}
		public virtual ICollection<ResellerImportReceiptDetails> ResellerImportReceiptDetails { get; set; }
		public virtual ICollection<ExportReceiptDetails> ExportReceiptDetails { get; set; }
		public virtual ICollection<ResellerStorage> ResellerStorage { get; set; }
	}
}
