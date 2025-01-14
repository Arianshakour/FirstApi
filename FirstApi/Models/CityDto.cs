namespace FirstApi.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Des { get; set; }
        public int NumOfPOI { get { return POIs.Count; } }
        //faqat get zadim ke readonly bashe va asan nemikhaim to DB zakhire beshe faqat baraye namayeshe
        public ICollection<PointOfInterestDto> POIs { get; set;} = new List<PointOfInterestDto>();
    }
}
