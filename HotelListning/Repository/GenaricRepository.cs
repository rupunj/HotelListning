using System;
using HotelListning.Contracts;
using HotelListning.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelListning.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : class
    {
        private HotelListningDbContext _context;

        public GenaricRepository(HotelListningDbContext context )
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> Exsist(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? Id)
        {
            if (Id is null)
            {
                return null;
            }
            return await _context.Set<T>().FindAsync(Id);
        }

        public async  Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
    }
}

