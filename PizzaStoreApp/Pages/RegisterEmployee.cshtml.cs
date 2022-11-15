using BusinessService;
using BusinessService.DTOs;
using BusinessService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using PizzaStoreApp.Helper;

namespace PizzaStoreApp.Pages
{
    public class RegisterEmployeeModel : PageModel
    {
        private readonly ILogger<RegisterEmployeeModel> _logger;
        private readonly IAccountService _accountService;

        public RegisterEmployeeModel(ILogger<RegisterEmployeeModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
            Account = new Account();
        }
        public string? ErrorMessage;
        [BindProperty]
        public Account? Account { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Account != null)
                {
                    var register = await _accountService.Register(Account);
                    if (register != null)
                    {
                        var session = HttpContext.Session;
                        SessionExtension.Set(session, "login-user", register);
                        ErrorMessage = null;
                        Cart cart = new Cart();
                        cart.Account = register;
                        SessionExtension.Set(session, "cart", cart);
                        return RedirectToPage("/Customer/HomePage");
                    } else
                    {
                        ErrorMessage = "Registered failed!";
                        return Page();
                    }
                }
                
            }
            else
            {
                ErrorMessage = "Register's data is not valid!";
            }
            return Page();
        }
    }
}
