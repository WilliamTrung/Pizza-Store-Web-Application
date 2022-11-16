using ApplicationCore.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessService
{
    public partial class Cart
    {
        public DTOs.Account Account { get; set; }
        public List<DTOs.Product> Products { get; set; }


        public Cart()
        {
            Account = new DTOs.Account();
            Products = new List<DTOs.Product>();
        }

    }
}
