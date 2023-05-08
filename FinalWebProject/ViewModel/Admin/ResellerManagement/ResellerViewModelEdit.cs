using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Admin.ResellerManagement
{
	public class ResellerViewModelEdit
	{
		[Required]
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
	}
}
