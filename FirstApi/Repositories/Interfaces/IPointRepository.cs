using FirstApi.Entities;

namespace FirstApi.Repositories.Interfaces
{
    public interface IPointRepository
    {
        Task<bool> CityExistAsync(int id);
        Task<City?> GetCityAsync(int id, bool includePoints);
        Task<IEnumerable<PointOfInterest>> GetPointsAsync(int cityid);
        Task<PointOfInterest?> GetPointAsync(int cityid, int pointofinterestid);
        Task CreateAsync(int cityid, PointOfInterest point);
        Task SaveAsync();
        void Edit(PointOfInterest point);
        void Delete(PointOfInterest point);
    }
}
