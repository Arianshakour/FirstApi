using FirstApi.Context;
using FirstApi.Entities;
using FirstApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories.Implementation
{
    public class CityRepository : ICityRepository
    {
        private readonly CityContext _context;

        public CityRepository(CityContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.City.ToListAsync();
        }

        public async Task<City?> GetCityAsync(int id, bool includePoints)
        {
            if (includePoints)
            {
                return await _context.City.Include(c=>c.POIs).FirstOrDefaultAsync(c=>c.Id== id);
            }
            return await _context.City.FirstOrDefaultAsync(c=>c.Id==id);
        }
        public async Task CreateAsync(City city)
        {
            await _context.City.AddAsync(city);
        }
        public void Edit(City city)
        {
            _context.City.Update(city);
        }
        public void Delete(City city)
        {
            _context.City.Remove(city);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
