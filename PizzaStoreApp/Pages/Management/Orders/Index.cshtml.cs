using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models2;

namespace PizzaStoreApp.Pages.Management.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationCore.Models2.PizzaStoreContext _context;

        public IndexModel(ApplicationCore.Models2.PizzaStoreContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Account).ToListAsync();
            }
        }
    }
}
