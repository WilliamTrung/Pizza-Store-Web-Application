using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessService.IService;
using BusinessService.DTOs;
using PizzaStoreApp.Helper;
using PizzaStoreApp.Filter;

namespace PizzaStoreApp.Pages.Management.Accounts
{
    [Authorized("0")]
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
        [BindProperty]
        public Account Account { get; set; } = default!;
        public string Message { get; set; } = "";
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Message = "";
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountService.GetDTOs(filter: a => a.AccountId == id);

            if (account == null)
            {
                return RedirectToPage("./Index");
            }
            
            Account = account.FirstOrDefault();
            SetViewDataRoles();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = Account;
            var login = SessionExtension.GetLoginUser(HttpContext.Session);
            if(login != null)
            {
                if (account.AccountId == login.AccountId && account.Type != login.Type)
                {
                    //cancel
                    Message = "Cannot update your type!";
                    SetViewDataRoles();
                    return Page();
                } else
                {
                    var updated = await _accountService.Update(filter: a => a.AccountId == account.AccountId, account);
                    if(updated != null)
                    {
                        Message = "Update successfully!";
                        SetViewDataRoles();
                        return Page();
                    }
                }
            }
            return RedirectToPage("./Index");
        }

        private bool AccountExists(int id)
        {
            return false;
        }
    }
}
