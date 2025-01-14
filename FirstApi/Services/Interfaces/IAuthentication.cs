using FirstApi.Entities;

namespace FirstApi.Services.Interfaces
{
    public interface IAuthentication
    {
        public CityUser Validation(string? username, string? password);
        public string GenerateToken(CityUser user);
    }
}
