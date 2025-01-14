using AutoMapper;
using FirstApi.Entities;
using FirstApi.Models;

namespace FirstApi.AutoMapperProfiles
{
    public class PointOfInterestProfile : Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<PointOfInterest, PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreateDto,Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForEditDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestForEditDto>();
        }
    }
}
