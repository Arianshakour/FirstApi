using AutoMapper;

namespace FirstApi.AutoMapperProfiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityDto>();
            //baraye har jadval bayad y class doros koni injoori
            // avali mabdae va dovomi maqsade
            //vali baraye estefade to citycontroller omadim aval khoroji method midim badesh vorodi
        }
    }
}
