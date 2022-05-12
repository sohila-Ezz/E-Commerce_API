using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API11.DTO
{
    public class CreateOrderDetailesDto
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
    }
}
