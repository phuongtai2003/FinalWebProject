using Microsoft.Build.Framework;

namespace FinalWebProject.ViewModel.Authentication
{
    public class AccountantRegisterViewModel
    {
        [Required]
        public string AcccountantName { get; set; }
        [Required]
        public string AccountantEmail { get; set; }
        [Required]
        public string AccountantPassword { get; set; }
        [Required]
        public int WarehouseID { get; set; }
    }
}
