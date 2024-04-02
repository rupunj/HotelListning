using System.Net;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelListning.Data.Configuration;
namespace HotelListning.Data
{
	public class HotelListningDbContext : IdentityDbContext<ApiUser>
    {
		public HotelListningDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
  
        }
    }
}

