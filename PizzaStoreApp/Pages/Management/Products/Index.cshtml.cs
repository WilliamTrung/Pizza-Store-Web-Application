using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using BusinessService.DTOs;
using Microsoft.AspNetCore.Authorization;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Products
{
    [Authorized("0")]
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get; set; } = default!;
        
        public async Task OnGetAsync()
        {
            /*
            if (_context.Products != null)
            {
                Product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier).ToListAsync();
            }
            */
            var products = await _productService.GetDTOs(includeProperties: "Category,Supplier");
            Product = (IList<Product>)products.ToList();
        }
    }
}
