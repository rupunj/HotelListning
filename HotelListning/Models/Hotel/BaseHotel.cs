using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListning.Models.Hotel
{
	public abstract class BaseHotel
	{
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}

