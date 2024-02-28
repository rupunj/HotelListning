using System;
using HotelListning.Data;
using HotelListning.Models.Hotel;

namespace HotelListning.Contracts
{
	public interface IHotelsRepository :IGenaricRepository<Hotel>
	{
		Task<Hotel> GetHotelFullDetails(int id)
;	}
}

