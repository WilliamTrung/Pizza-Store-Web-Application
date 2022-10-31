using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessService.DTOs
{
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Company name must not be empty!")]
        [MinLength(1, ErrorMessage = "Name is too short!")]
        public string CompanyName { get; set; } = null!;
        [Required(ErrorMessage ="Address must not be empty!")]
        [MinLength(1, ErrorMessage ="Address is too short!")]
        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "Address must not be empty!")]
        [StringLength(10, ErrorMessage = "Phone number is not valid!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must only contain numbers!")]
        public string Phone { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
