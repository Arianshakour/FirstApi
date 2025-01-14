using FirstApi.Entities;
using FirstApi.Models;

namespace FirstApi.Repositories.Interfaces
{
    public interface ICityRepository
    {
        //IEnumerable<City> GetCities();
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int id , bool includePoints);
        Task CreateAsync(City city);
        void Edit(City city);
        void Delete(City city);
        Task SaveAsync();
    }
}
