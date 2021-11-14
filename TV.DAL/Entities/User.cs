using Microsoft.AspNetCore.Identity;

namespace TV.DAL.Entities
{
    public class User : IdentityUser
    {
        public string ProfileImageUrl { get; set; }
        public string Name { get; set; }
        public string FederalRegistration { get; set;}
        public string Adress { get; set; }
        
    }
}