using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API11.DTO
{
    public class CreateOrderDto
    {
        public DateTime Date { get; set; }
        [Required]
        public decimal TotelPrice { get; set; }
        [Required]
        public bool IsDone { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
