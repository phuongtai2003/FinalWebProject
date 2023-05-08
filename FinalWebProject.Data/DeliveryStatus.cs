using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class DeliveryStatus
	{
		[Key]
		public int DeliveryStatusId { get; set; }
		public string DeliveryStatusName { get; set; }
		public virtual ICollection<ResellerImportReceipt> ResellerImportReceipts { get; set; }
	}
}
