using System.ComponentModel.DataAnnotations;

namespace FirstApi.Entities
{
    public class CityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Family { get; set; }
        [MaxLength(50)]
        public string City { get; set; }

    }
}
