using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using PizzaStoreApp.Helper;
using BusinessService.DTOs;

namespace PizzaStoreApp.Pages.Customer.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;

        public IndexModel(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }

        public IList<Order> Order { get;set; } = default!;
        

        public async Task<IActionResult> OnGetAsync()
        {
            Order = new List<Order>();
            var login = SessionExtension.GetLoginUser(HttpContext.Session);
            if(login == null)
            {
                return RedirectToPage("Index");
            }
            var orders = await _orderService.GetDTOs(filter: o => o.AccountId == login.AccountId, includeProperties: "Account");

            if(orders != null && orders.Count() > 0)
            {
                Order = orders.ToList();
            }
            return Page();
        }
    }
}
