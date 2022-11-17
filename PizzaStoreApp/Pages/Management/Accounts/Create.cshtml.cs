using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessService.IService;
using BusinessService.DTOs;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Accounts
{
    [Authorized("0")]
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            Message = "";
            var roles = new List<Role>();
            roles.Add(new Role()
            {
                Id = 0,
                Name = "Administrator"
            });
            roles.Add(new Role()
            {
                Id = 1,
                Name = "Customer"
            });
            ViewData["Type"] = new SelectList(roles, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }

        public string Message { get; set; }
        public class Role
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }
        private void SetViewDataRoles()
        {
            var roles = new List<Role>();
            roles.Add(new Role()
            {
                Id = 0,
                Name = "Administrator"
            });
            roles.Add(new Role()
            {
                Id = 1,
                Name = "Customer"
            });
            ViewData["Type"] = new SelectList(roles, "Id", "Name");
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            var created = await _accountService.Create(Account);
            if(created == null)
            {
                Message = "Failed to create account!";
                SetViewDataRoles();
                return Page();
            }
                

            return RedirectToPage("./Index");
        }
    }
}
