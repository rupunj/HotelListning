using System;
using HotelListning.Models.Country;

namespace HotelListning.Models.Hotel
{
	public class GetHotelCountryDto :BaseHotel
	{
		public int Id { get; set; }

		public GetCountryDetailsDto Country { get; set; }
	}
}

