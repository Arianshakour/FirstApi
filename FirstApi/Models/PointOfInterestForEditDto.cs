using System.ComponentModel.DataAnnotations;

namespace FirstApi.Models
{
    public class PointOfInterestForEditDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
