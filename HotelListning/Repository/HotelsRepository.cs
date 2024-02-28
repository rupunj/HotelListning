using System;
using HotelListning.Contracts;
using HotelListning.Data;
using HotelListning.Models.Hotel;
using Microsoft.EntityFrameworkCore;

namespace HotelListning.Repository
{
	public class HotelsRepository : GenaricRepository<Hotel> , IHotelsRepository
    {
		private readonly HotelListningDbContext _context;
		public HotelsRepository(HotelListningDbContext context) :base (context)
		{
			_context = context;
		}

		public async Task<Hotel> GetHotelFullDetails(int Id)
		{
			return await _context.Hotels.Include(p => p.Country).FirstOrDefaultAsync(p => p.Id == Id);
		}


    }
}

