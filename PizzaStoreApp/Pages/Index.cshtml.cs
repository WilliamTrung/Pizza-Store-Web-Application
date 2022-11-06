using BusinessService.DTOs;
using BusinessService.IService;
using BusinessService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public IndexModel(ILogger<IndexModel> logger, IProductService _productService, ICategoryService _categoryService)
        {
            _logger = logger;
            productService = _productService;
            categoryService = _categoryService;
        }
        public Product product { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }
        /*public IActionResult OnGet()
        {
            if(SessionExtension.GetLoginUser(HttpContext.Session) == null)
            {
                return RedirectToPage("LoginEmployee");
            } else
            {
                return RedirectToPage("Management/Products/Index");
            }
        }*/
        public async Task OnGetAsync()
        {
            try
            {
                var products = await productService.GetDTOs(includeProperties: "Category,Supplier");
                Products = (IList<Product>)products.ToList();
                Products = Products.ToList();
         /*       Products = Products.OrderBy(d => d.ProductId).Take(6).ToList();*/

                var categories = await categoryService.GetDTOs();
                Categories = (IList<Category>)categories.ToList();
                Categories = Categories.ToList();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
        }
    }
}