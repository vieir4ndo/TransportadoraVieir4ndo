using Microsoft.AspNetCore.Http;

namespace TV.API.ViewModels
{
    public class UpdateUserViewModel
    {
        public string Email {get; set;}
        public IFormFile ProfileImage { get; set; }
        public string Name {get; set;}
        public string FederalRegistration { get; set;}
        public string Adress { get; set; }
    }
}