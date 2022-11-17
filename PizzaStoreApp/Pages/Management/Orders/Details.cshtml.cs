using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using BusinessService.DTOs;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Orders
{
    [Authorized("0")]
    public class DetailsModel : PageModel
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;

        public DetailsModel(IOrderDetailService orderDetailService, IOrderService orderService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
        }

        public Order Order { get; set; }
        public IList<OrderDetail> OrderDetail { get; set; } = default!;
        public decimal Total { get; set; } = 0;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Management/Orders/Index");
            }
            var order = await _orderService.GetDTOs(filter: o => o.OrderId == id);
            if (order == null)
            {
                return RedirectToPage("/Management/Orders/Index");
            }
            Order = order.First();

            var details = await _orderDetailService.GetDTOs(filter: detail => detail.OrderId == id, includeProperties: "Product");
            OrderDetail = details.ToList();

            foreach (var detail in OrderDetail)
            {
                Total += (detail.Quantity * detail.UnitPrice);
                detail.UnitPrice = Math.Round(detail.UnitPrice, 0);
            }
            Total = Math.Round(Total, 0);
            return Page();
        }
    }
}
