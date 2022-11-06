using BusinessService.DTOs;
using BusinessService.IService;
using BusinessService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;
using System.ComponentModel.DataAnnotations;

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
        public Product product { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Category> Categories { get; set; }

        public async Task OnGetAsync()
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
           
        }
    }
}
