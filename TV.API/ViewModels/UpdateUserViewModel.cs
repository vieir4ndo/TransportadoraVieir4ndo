using Microsoft.AspNetCore.Http;

namespace TV.API.ViewModels
{
    public class UpdateUserViewModel
    {
        public IFormFile ProfileImage { get; set; }
    }
}