using BusinessService;
using BusinessService.DTOs;
using BusinessService.IService;
using BusinessService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PizzaStoreApp.Pages
{
    public class HomePageModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public HomePageModel(ILogger<IndexModel> logger, IProductService _productService, ICategoryService _categoryService)
        {
            _logger = logger;
            productService = _productService;
            categoryService = _categoryService;
        }
        [BindProperty]
        [Required(ErrorMessage = "Quantity must not be empty!")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity must be greater than 0!")]
        public int Quantity { get; set; } = 0;
        public string Message { get; set; } = "";
        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

                try
                {
                    var products = await productService.GetDTOs(includeProperties: "Category,Supplier");
                    Products = (IList<Product>)products.ToList();
                    Products = Products.ToList();

                    var categories = await categoryService.GetDTOs();
                    Categories = (IList<Category>)categories.ToList();
                    Categories = Categories.ToList();

                }
                catch (Exception ex)
                {
                    TempData["Message"] = ex.Message;
                }
            return Page();
        }
        public async Task<IActionResult> OnPost(int id, int quantity)
        {
            var products = await productService.GetDTOs(filter: p => p.ProductId == id, includeProperties: "Category,Supplier");
            if(products == null || quantity <= 0 || products.Count() <= 0)
            {
                Message = "Error selecting product!";
                return await OnGetAsync();
            }
            var product = products.First();
            product.QuantityPerUnit = quantity;
            Cart cart = SessionExtension.GetCart(HttpContext.Session);
            if (cart == null)
            {
                Message = "Error getting cart!";
            } else
            {
                //add
                //find
                var find = cart.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if(find != null)
                {
                    find.QuantityPerUnit += product.QuantityPerUnit;
                } else
                {
                    cart.Products.Add(product);
                }
                
            }
               
            SessionExtension.Set<Cart>(HttpContext.Session, "cart", cart);
            return await OnGetAsync();
        }
    }
}
