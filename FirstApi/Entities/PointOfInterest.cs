using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApi.Entities
{
    public class PointOfInterest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Des { get; set; }
        //Constructor
        public PointOfInterest(string name)
        {
            this.Name = name;
        }
        //relation
        [ForeignKey("CityId")]
        public City? City { get; set; }  //inja ertebat barqarar shode vali nagofti rooye kodom field 
        //ba in [ForeignKey("CityId")] gofti roye kodom field bashe
        public int CityId { get; set; }

    }
}
