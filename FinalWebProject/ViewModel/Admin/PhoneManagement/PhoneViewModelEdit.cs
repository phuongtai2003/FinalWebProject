using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Admin.PhoneManagement
{
    public class PhoneViewModelEdit
    {
        public int PhoneId { get; set; }
        [Required]
        public string PhoneName { get; set; }
        [Required]
        public string PhoneDescription { get; set; }
        [Required]
        [Range(1990, 2023, ErrorMessage = "Invalid Released Year")]
        public int PhoneYear { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Invalid Price Value")]
        public int Price { get; set; }
        public int ManufacturerId { get; set; }
    }
}
