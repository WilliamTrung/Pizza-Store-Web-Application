
using ApplicationCore.Models2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.IService
{
    public interface IAccountService : IBaseService<Account, DTOs.Account>
    {
        public Task<DTOs.Account?> Login(DTOs.Account account);
        public Task<DTOs.Account?> Register(DTOs.Account account);
    }
}
