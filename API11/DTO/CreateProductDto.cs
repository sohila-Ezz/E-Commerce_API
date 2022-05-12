﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API11.DTO
{
    public class CreateProductDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3, ErrorMessage = "Name must be greater than 3 char")]
        public string Name { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        //[RegularExpression(@"^\w+\.(png|jpg)$")]
        public IFormFile? Image { get; set; }
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int Category_Id { get; set; }

    }
}
