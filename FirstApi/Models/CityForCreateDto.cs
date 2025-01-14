using System.ComponentModel.DataAnnotations;

namespace FirstApi.Models
{
    public class CityForCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Des { get; set; }
    }
}
