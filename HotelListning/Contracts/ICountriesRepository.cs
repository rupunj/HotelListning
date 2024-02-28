using System;
using HotelListning.Data;

namespace HotelListning.Contracts
{
    public interface ICountriesRepository : IGenaricRepository<Country>
    {
        public Task<Country> GetDetails(int Id);
    }
}

