using System.ComponentModel.DataAnnotations;

namespace TV.API.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}