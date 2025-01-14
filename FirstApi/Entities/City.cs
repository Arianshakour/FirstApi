using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApi.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //nazashti ham khodesh otomat identity mikone
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Des { get; set; }

        //Constructor
        public City(string name)
        {
            this.Name = name;
        }
        public ICollection<PointOfInterest> POIs { get; set; } = new List<PointOfInterest>();
    }
}
