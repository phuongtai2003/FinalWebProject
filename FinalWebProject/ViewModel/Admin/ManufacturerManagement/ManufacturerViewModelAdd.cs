using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Admin.ManufacturerManagement
{
	public class ManufacturerViewModelAdd
	{
		[Required]
		public string ManufacturerName { get; set; }
		[Required]
		[Range(1800, 2023)]
		public int ManufacturerYear { get; set; }
	}
}
