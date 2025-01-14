using FirstApi.Models;

namespace FirstApi
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CityDataStore _context { get;} = new CityDataStore();
        //baraye inke faqat readonly bashe get gozashtim va set nazashtim
        //albate age ba DI benevisi dg ehtiaji be in nis va ba service add mikoni
        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Tehran",
                    Des = "This is my City",
                    POIs = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=1,
                            Name="p1",
                            Description = "d1"
                        },new PointOfInterestDto()
                        {
                            Id=2,
                            Name="p2",
                            Description = "d2"
                        }
                    }
                },new CityDto()
                {
                    Id = 2,
                    Name = "Esfehan",
                    Des = "This is my City",
                    POIs= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=3,
                            Name="p3",
                            Description = "d3"
                        },new PointOfInterestDto()
                        {
                            Id=4,
                            Name="p4",
                            Description = "d4"
                        }
                    }
                },new CityDto()
                {
                    Id = 3,
                    Name = "Shiraz",
                    Des = "This is my City",
                    POIs= new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id=5,
                            Name="p5",
                            Description = "d5"
                        },new PointOfInterestDto()
                        {
                            Id=6,
                            Name="p6",
                            Description = "d6"
                        }
                    }
                }
            };
        }
    }
}
