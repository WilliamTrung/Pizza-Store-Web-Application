using System;
using System.Collections.Generic;

namespace BusinessService.DTOs
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? Freight { get; set; }
        public string ShipAddress { get; set; } = null!;

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
