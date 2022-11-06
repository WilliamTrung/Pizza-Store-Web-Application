using BusinessService.DTOs;
using BusinessService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;
using System.ComponentModel.DataAnnotations;

namespace PizzaStoreApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            if (SessionExtension.GetLoginUser(HttpContext.Session) == null)
            {
                return RedirectToPage("LoginEmployee");
            }
            else
            {
                return RedirectToPage("Management/Products/Index");
            }
        }
    }
}
