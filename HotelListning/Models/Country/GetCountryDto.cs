using System;
using HotelListning.Models.Hotel;

namespace HotelListning.Models
{
	public class GetCountryDto :BaseCountry
	{
		public int Id { get; set; }
		
	}
	public class GetCoutryDetailsDto :BaseCountry
	{
        public int Id { get; set; }
        
		public List<GetHotelDto> Hotels { get; set; }
    }
}

