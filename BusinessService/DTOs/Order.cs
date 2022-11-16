using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessService.DTOs
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        [DisplayName("Date")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Required Date")]
        public DateTime RequiredDate { get; set; }
        [DisplayName("Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        [DisplayName("Freight")]
        public string? Freight { get; set; }
        [DisplayName("Ship Address")]
        public string ShipAddress { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
