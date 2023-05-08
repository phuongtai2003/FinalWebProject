using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class ResellerStorage
	{
		[Required]
		public int Quantity { get; set; }
		public int ResellerId { get; set;}
		public Reseller Reseller { get; set;}
		public int PhoneId { get; set;}
		public Phone Phone { get; set;}
	}
}
