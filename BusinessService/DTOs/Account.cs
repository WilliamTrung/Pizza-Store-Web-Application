using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BusinessService.DTOs
{
    public partial class Account
    {
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "Username")]
        [MinLength(8, ErrorMessage = "Username must be at least 8 characters")]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = null!;
        [Required]
        [Display(Name = "Full name")]
        [MinLength(8, ErrorMessage = "Fullname must be at least 8 characters")]
        public string FullName { get; set; } = null!;
        public int Type { get; set; }
    }
}
