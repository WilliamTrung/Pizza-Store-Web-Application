using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BusinessService.DTOs
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
        }
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "Username")]
        [MinLength(8, ErrorMessage = "Username must be at least 8 characters")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = null!;
        [Required]
        public int Type { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [StringLength(10, ErrorMessage = "Length must be equal to 10!")]
        public string Phone { get; set; } = null!;

        

        public virtual ICollection<Order> Orders { get; set; }
    }
}
