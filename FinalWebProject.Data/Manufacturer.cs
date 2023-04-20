using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalWebProject.Data
{
	public class Manufacturer
	{
		[Key]
		public int ManufacturerId { get; set; }
		[Required]
		public string ManufacturerName { get; set;}
		[Required]
		public int ManufacturerYear { get; set;}
		public virtual ICollection<Phone> Phones { get; set;}
	}
}
