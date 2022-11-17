using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using BusinessService.DTOs;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Accounts
{
    [Authorized("0")]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IList<Account> Account { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Account = new List<Account>();
            var accounts = await _accountService.GetDTOs();
            if(accounts != null && accounts.Count() > 0)
            {
                Account = accounts.ToList();
            }

        }
    }
}
