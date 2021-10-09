using System.ComponentModel.DataAnnotations;

namespace TV.API.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}