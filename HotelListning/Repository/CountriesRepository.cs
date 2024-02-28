using System;
using HotelListning.Contracts;
using HotelListning.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListning.Repository
{
	public class CountriesRepository : GenaricRepository<Country>, ICountriesRepository
    {
		private readonly HotelListningDbContext _context;
		public CountriesRepository(HotelListningDbContext context):base (context)
		{
			_context = context;
		}
		public async Task<Country> GetDetails(int Id)
        {
			return  await _context.Countries.Include(p => p.Hotels).FirstOrDefaultAsync(q => q.Id == Id);
		}




    }
}

