using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Admin.ManufacturerManagement
{
	public class ManufacturerViewModelEdit
	{
		[Required]
		public int ManufacturerId { get; set; }
		[Required]
		public string ManufacturerName { get; set; }
		[Required]
		[Range(1800, 2023)]
		public int ManufacturerYear { get; set; }
	}
}
