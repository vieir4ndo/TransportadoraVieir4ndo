using TV.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TV.DAL
{
    public class ApplicationDbContex : IdentityDbContext<User>
    {
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options)
        : base(options) { }

        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
    }
    
}