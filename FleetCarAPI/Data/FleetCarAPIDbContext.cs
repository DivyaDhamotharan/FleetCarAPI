using FleetCarAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FleetCarAPI.Data
{
    public class FleetCarAPIDbContext : DbContext
    {
        public FleetCarAPIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Cars> FleetCars { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
