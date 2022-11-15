using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessService.DTOs
{ 
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        [Required]
        [MinLength(3, ErrorMessage ="Name must be at least 3 characters")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Supplier must not be empty!")]
        public int? SupplierId { get; set; }
        [Required(ErrorMessage = "Category must not be empty!")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Quantity must not be empty!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be greater than 0!")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int QuantityPerUnit { get; set; }
        [Required(ErrorMessage = "Price must not be empty!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price must be greater than 0!")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal UnitPrice { get; set; }
        public string? ProductImage { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
