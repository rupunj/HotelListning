using System.Net;
using Microsoft.EntityFrameworkCore;

namespace HotelListning.Data
{
	public class HotelListningDbContext :DbContext
	{
		public HotelListningDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Country>().HasData(
				new Country
				{
					Id = 1,
					Name = "Sri Lanka",
					ShortName = "LK"
				},
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = 3,
                    Name = "India",
                    ShortName = "IN"
                }) ;

            modelBuilder.Entity<Hotel>().HasData(new Hotel
            {
                Id = 1,
                Name = "Avenra",
                Address="Negambo",
                CountryId=1,
                Rating=4.3
            },
            new Hotel
            {
                Id = 2,
                Name = "Hotel India",
                Address = "Chennai",
                CountryId = 3,
                Rating = 4.8
            },
            new Hotel
            {
                Id = 3,
                Name = "Hotel Bahamas",
                Address = "Bahamas Town",
                CountryId = 2,
                Rating = 4.8
            });  
        }
    }
}

