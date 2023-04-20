using Microsoft.Build.Framework;

namespace FinalWebProject.ViewModel.Admin.WarehouseManagement
{
	public class WarehouseViewModelEdit
	{
		[Required]
		public int WarehouseId { get; set; }
		[Required]
		public string WarehouseName { get; set; }
		[Required]
		public string WarehouseLocation { get; set; }
	}
}
