using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudproject.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Description { get; set; } = default!;

        [Required]
        public double Price { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; } = default!;

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; } = default!;
    }
}