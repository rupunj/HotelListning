using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListning.Data.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(new Hotel
            {
                Id = 1,
                Name = "Avenra",
                Address = "Negambo",
                CountryId = 1,
                Rating = 4.3
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

