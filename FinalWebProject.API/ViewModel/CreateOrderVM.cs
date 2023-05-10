using FinalWebProject.Data;

namespace FinalWebProject.API.ViewModel
{
	public class CreateOrderVM
	{
		public List<PhoneVM> Phones { get; set; }
		public List<int> Quantity { get; set; }
	}
}
