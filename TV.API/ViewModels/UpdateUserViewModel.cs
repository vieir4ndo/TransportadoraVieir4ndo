using Microsoft.AspNetCore.Http;

namespace TV.API.ViewModels
{
    public class UpdateUserViewModel
    {
        public string Email {get; set;}
        public IFormFile ProfileImage { get; set; }
    }
}