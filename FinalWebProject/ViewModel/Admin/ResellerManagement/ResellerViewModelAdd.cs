using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Admin.ResellerManagement
{
	public class ResellerViewModelAdd
	{
		[Required]
		public string ResellerName { get; set; }
		[Required]
		[EmailAddress]
		public string ResellerEmail { get; set; }
		[Required]
		public string ResellerPassword { get; set; }
		[Required]
		public string ResellerLocation { get; set; }
	}
}
