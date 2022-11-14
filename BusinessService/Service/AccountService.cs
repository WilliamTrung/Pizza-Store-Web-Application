using ApplicationCore.Models2;
using ApplicationCore.Repository;
using AutoMapper;
using BusinessService.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Service
{
    public class AccountService : BaseService<Account, DTOs.Account>, IAccountService
    {
        public AccountService(PizzaStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<DTOs.Account?> Login(DTOs.Account account)
        {
            //throw new NotImplementedException();
            var find = await GetDTOs(filter: a => a.Username == account.Username && a.Password == account.Password);
            var found = find.FirstOrDefault();
            if(found != null)
            {
                return found;
            }
            return null;
        }

        public async Task<DTOs.Account?> Register(DTOs.Account account)
        {
            //throw new NotImplementedException();
            var find = await GetDTOs(filter: a => a.Username == account.Username && a.Password == account.Password);
            var found = find.FirstOrDefault();
            if (found == null)
            {
                //set type to customer
                account.Type = 0;
                if(await Create(account))
                {
                    return account;
                }
            }
            return null;
        }
    }
}
