using TV.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TV.DAL
{
    public class ApplicationDbContex : DbContext
    {
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options)
        : base(options) { }

        public DbSet<Value> Values { get; set; }
    }
    
}