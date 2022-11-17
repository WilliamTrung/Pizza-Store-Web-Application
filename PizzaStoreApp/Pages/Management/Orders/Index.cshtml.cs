using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessService.IService;
using BusinessService.DTOs;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Orders
{
    [Authorized("0")]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService )
        {
            _orderService = orderService;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Order = new List<Order>();
            var orders = await _orderService.GetDTOs(includeProperties: "Account");
            if(orders != null && orders.Count() > 0)
            {
                Order = orders.ToList();
            }
        }
    }
}
