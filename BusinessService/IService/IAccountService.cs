using BusinessService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IService
{
    public interface IAccountService
    {
        public Task<Account?> Login(Account account);
        public Task<Account?> Register(Account account);
    }
}
