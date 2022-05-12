using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API11.Models
{
    public class OrderDetailes
    {
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public int ProductQuantity { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public virtual Product Product { get; set; }

    }
}
