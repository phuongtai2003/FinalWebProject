using System.ComponentModel.DataAnnotations;

namespace FinalWebProject.ViewModel.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Password must have at least 6 characters")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
