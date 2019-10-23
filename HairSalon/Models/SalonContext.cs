using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Salon.Models
{
    public class SalonContext : IdentityDbContext<ApplicationUser> 
    {
        public virtual DbSet<Stylist> Stylists { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<StylistClient> StylistClient { get; set; }


        public SalonContext(DbContextOptions options) : base(options) { }
    }
}