using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models2
{
    public partial class Account
    {
        public Account()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public int AccountId { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public int Type { get; set; }

        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        
        public string Phone { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
