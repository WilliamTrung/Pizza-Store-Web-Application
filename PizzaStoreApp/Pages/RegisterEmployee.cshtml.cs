using BusinessService.DTOs;
using BusinessService.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Account != null)
                {
                    var register = _accountService.Register(Account);
                    if (register != null)
                    {
                        var session = HttpContext.Session;
                        SessionExtension.Set(session, "login-user", register);
                        ErrorMessage = null;
                        ViewComponent("Index");
                    }
                }
            }
            else
            {
                ErrorMessage = "Register's data is not valid!";
            }
        }
    }
}
