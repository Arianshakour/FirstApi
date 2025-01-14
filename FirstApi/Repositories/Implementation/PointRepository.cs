using FirstApi.Context;
using FirstApi.Entities;
using FirstApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Repositories.Implementation
{
    public class PointRepository : IPointRepository
    {
        private readonly CityContext _context;

        public PointRepository(CityContext context)
        {
            _context = context;
        }
        public async Task<bool> CityExistAsync(int id)
        {
            return await _context.City.AnyAsync(x => x.Id == id);
        }
        public async Task<City?> GetCityAsync(int id, bool includePoints)
        {
            if (includePoints)
            {
                return await _context.City.Include(c => c.POIs).FirstOrDefaultAsync(c => c.Id == id);
            }
            return await _context.City.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<PointOfInterest?> GetPointAsync(int cityid, int pointofinterestid)
        {
            return await _context.PointOfInterest.
                FirstOrDefaultAsync(p => p.CityId == cityid && p.Id == pointofinterestid);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsAsync(int cityid)
        {
            return await _context.PointOfInterest.Where(p => p.CityId == cityid).ToListAsync();
        }
        public async Task CreateAsync(int cityid, PointOfInterest point)
        {
            var city = await GetCityAsync(cityid, false);
            if (city != null)
            {
                city.POIs.Add(point);
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Edit(PointOfInterest point)
        {
            _context.PointOfInterest.Update(point);
        }
        public void Delete(PointOfInterest point)
        {
            _context.PointOfInterest.Remove(point);
        }
    }
}
