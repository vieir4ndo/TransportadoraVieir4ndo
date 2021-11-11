using Microsoft.AspNetCore.Identity;

namespace TV.DAL.Entities
{
    public class User : IdentityUser
    {

        public string ProfileImageUrl { get; set; }
    }
}