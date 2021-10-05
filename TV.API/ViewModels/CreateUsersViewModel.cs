using System.ComponentModel.DataAnnotations;

namespace TV.API.ViewModels
{
    public class CreateUsersViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}