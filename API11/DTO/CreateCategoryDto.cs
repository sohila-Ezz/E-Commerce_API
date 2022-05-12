using System.ComponentModel.DataAnnotations;

namespace API11.DTO
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3, ErrorMessage = "Name must be greater than 3 char")]
        public string Name { get; set; }
    }
}
