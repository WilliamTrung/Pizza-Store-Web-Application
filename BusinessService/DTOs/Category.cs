using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessService.DTOs
{ 
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category name must not be empty!")]
        [MinLength(1, ErrorMessage = "Category name is too short!")]
        public string CategoryName { get; set; } = null!;
        [Required(ErrorMessage = "Description must not be empty!")]
        [MinLength(1, ErrorMessage = "Description is too short!")]
        public string Description { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
