using BusinessService.DTOs;
using BusinessService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaStoreApp.Helper;

namespace PizzaStoreApp.Pages
{
    public class LoginEmployeeModel : PageModel
    {
        private readonly ILogger<LoginEmployeeModel> _logger;
        private readonly IAccountService _accountService;

        public LoginEmployeeModel(ILogger<LoginEmployeeModel> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        public string? ErrorMessage;
        [BindProperty]
        public Account? Account { get; set; }
        public void OnGet()
        {
            Account = new Account();
        }
        public IActionResult? OnPost()
        {
            if (Account != null)
            {
                var login = _accountService.Login(Account);
                if (login != null)
                {
                    var session = HttpContext.Session;
                    SessionExtension.Set(session, "login-user", login.Result);
                    ErrorMessage = null;
                    return RedirectToPage("Index");
                }
            }
            ErrorMessage = "Wrong username or password";
            return default;
        }
    }
}
